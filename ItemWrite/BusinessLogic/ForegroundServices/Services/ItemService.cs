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

        public Task<int> GetCount(TItem item)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Push<T>(T item)
        {
            using (var producer = new ProducerBuilder<string, T>
                 (new ProducerConfig() { BootstrapServers = _configuration["KafkaServer:BootstrapServers"] }).Build())
            {
                try
                {
                    var x = await producer.ProduceAsync
                       ("items", new Message<string, T>
                       {
                           Value = item,
                           Key = "item_write"
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
            throw new NotImplementedException();
        }
    }
}
