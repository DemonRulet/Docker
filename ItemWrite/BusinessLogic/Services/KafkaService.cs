using BusinessLogic.Interfaces;
using Kafka.Enums;
using Microsoft.Extensions.Configuration;
using Kafka.Configuration;
using System.Threading.Tasks;
using Confluent.Kafka;
using System;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class KafkaService<T> : IKafkaService<T>
    {
        private readonly ILogger<KafkaService<T>> _logger;

        public KafkaService(IConfiguration configuration, ILogger<KafkaService<T>> logger)
        {
            _logger = logger;
        }

        public async Task<int> Push(T item)
        {
            using (var producer = new ProducerBuilder<string, T>
                (_producerConfig).Build())
            {
                try
                {
                    await producer.ProduceAsync
                       (_topicConfig.Name, new Message<string, T>
                       {
                           Key = _messageConfig.Key,
                           Value = item,
                       });

                    return StatusCode.Success;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                }
            }
            return StatusCode.Error;
        }

        private void ReadConfiguration(out ProducerConfig producerConfig, IConfiguration configuration)
        {
            producerConfig = new ProducerConfig()
            {
                BootstrapServers = configuration["ItemWriteProducerConfig:BootstrapServers"],
            };
        }

        private void ReadConfiguration(out TopicConfig topicConfig, IConfiguration configuration)
        {
            topicConfig = new TopicConfig()
            {
                Name = configuration["ItemWriteTopicConfig:Name"],
            };
        }

        private void ReadConfiguration(out MessageConfig messageConfig, IConfiguration configuration)
        {
            messageConfig = new MessageConfig()
            {
                Key = configuration["ItemWriteMessageConfig:Key"],
            };
        }
    }
}
