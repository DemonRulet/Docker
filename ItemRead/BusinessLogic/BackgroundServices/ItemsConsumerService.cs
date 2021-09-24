using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BusinessLogic.ForegroundServices.Interfaces;

namespace BusinessLogic.BackgroundServices
{
    public class ItemsConsumerService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly IItemService<string> _itemService;
        private readonly ILogger<ItemsConsumerService> _logger;
        private readonly IItemProducerService _itemProducerService;

        public ItemsConsumerService(
            IConfiguration configuration,
            IItemService<string> itemService,
            ILogger<ItemsConsumerService> logger,
            IItemProducerService itemProducerService)
        {
            _configuration = configuration;
            _logger = logger;
            _itemService = itemService;
            _itemProducerService = itemProducerService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var conf = new ConsumerConfig
            {
                GroupId = _configuration["ItemsConsumer:GroupId"],
                BootstrapServers = _configuration["KafkaServer:BootstrapServers"],
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            using (var builder = new ConsumerBuilder<Ignore,
                string>(conf).Build())
            {
                builder.Assign(new TopicPartition(_configuration["ItemsConsumer:Topic"],
                    Convert.ToUInt16(_configuration["ItemsConsumer:TopicPartition"])));
                var cancelToken = new CancellationTokenSource();
                try
                {
                    while (true)
                    {
                        var consumer = builder.Consume(cancelToken.Token);
                        await _itemService.Add(consumer.Message.Value);
                        await _itemProducerService.SendToKafka(consumer.Message.Value);
                        _logger.LogInformation("Message: {0} partition {1} received from {2}",
                           consumer.Message.Value, consumer.Partition.Value, consumer.TopicPartitionOffset);
                    }
                }
                catch (Exception exception)
                {
                    _logger.LogError("[{0}] terminated with an error [{1}].",
                        nameof(ItemsConsumerService), exception.Message);
                    builder.Close();
                }
            }
            return;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
