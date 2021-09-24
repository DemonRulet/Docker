using BusinessLogic.ForegroundServices.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BusinessLogic.ForegroundServices.Services
{
    public class ItemProduceService : IItemProducerService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ItemProduceService> _logger;

        public ItemProduceService(
            IConfiguration configuration,
            ILogger<ItemProduceService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<int> SendToKafka<TItem>(TItem item)
        {
            using (var producer = new ProducerBuilder<Null, TItem>
               (new ProducerConfig() { BootstrapServers = _configuration["KafkaServer:BootstrapServers"] }).Build())
            {
                try
                {
                    var x = await producer.ProduceAsync
                       (new TopicPartition(_configuration["ItemsProducer:Topic"],
                       Convert.ToUInt16(_configuration["ItemsProducer:TopicPartition"])), new Message<Null, TItem>
                       {
                           Value = item,
                       });

                    return 1;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                }
            }
            return 0;
        }
    }
}
