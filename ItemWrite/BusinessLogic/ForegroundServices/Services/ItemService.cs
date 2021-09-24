using BusinessLogic.ForegroundServices.Interfaces;
using Confluent.Kafka;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BusinessLogic.ForegroundServices.Services
{
    public class ItemService<TItem> : IItemService<TItem>, IItemServiceKafka
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ItemService<TItem>> _logger;
        private readonly IItemRepository<TItem> _itemRepository;

        public ItemService(
            IConfiguration configuration,
            ILogger<ItemService<TItem>> logger,
            IItemRepository<TItem> itemRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _itemRepository = itemRepository;
        }

        public Task<int> Add(TItem item)
        {
            return Push(item);
        }

        public Task<int> GetCount()
        {
            return _itemRepository.Count();
        }

        public async Task<int> Push<T>(T item)
        {
            using (var producer = new ProducerBuilder<Null, T>
                 (new ProducerConfig() { BootstrapServers = _configuration["KafkaServer:BootstrapServers"] }).Build())
            {
                try
                {
                    var x = await producer.ProduceAsync
                       (new TopicPartition(_configuration["ItemsProducer:Topic"], 
                       Convert.ToUInt16(_configuration["ItemsProducer:TopicPartition"])), new Message<Null, T>
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

        public Task<int> UpdateCount(TItem item)
        {
            return _itemRepository.UpdateCount(item);
        }
    }
}
