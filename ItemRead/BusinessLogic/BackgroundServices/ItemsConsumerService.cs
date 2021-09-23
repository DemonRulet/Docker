using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using DataAccess.Interfaces;

namespace BusinessLogic.BackgroundServices
{
    public class ItemsConsumerService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly IItemRepository<string> _itemRepository;

        public ItemsConsumerService(IConfiguration configuration, IItemRepository<string> itemRepository)
        {
            _configuration = configuration;
            _itemRepository = itemRepository;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var conf = new ConsumerConfig
            {
                GroupId = _configuration["ItemsConsumer:GroupId"],
                BootstrapServers = _configuration["ItemsConsumer:BootstrapServers"],
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };
            using (var builder = new ConsumerBuilder<Ignore,
                string>(conf).Build())
            {
                builder.Subscribe(_configuration["ItemsConsumer:Topic"]);
                var cancelToken = new CancellationTokenSource();
                try
                {
                    while (true)
                    {
                        Console.WriteLine("Test");
                        var consumer = builder.Consume(cancelToken.Token);
                        await _itemRepository.Add(consumer.Message.Value);
                        Console.WriteLine($"Message: {consumer.Message.Value} partition {consumer.Partition.Value} received from {consumer.TopicPartitionOffset}");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("End");
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
