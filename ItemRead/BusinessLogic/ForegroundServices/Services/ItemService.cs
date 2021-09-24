using BusinessLogic.ForegroundServices.Interfaces;
using Confluent.Kafka;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BusinessLogic.ForegroundServices.Services
{
    public class ItemService<TItem> : IItemService<TItem>
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
            return _itemRepository.Add(item);
        }
    }
}
