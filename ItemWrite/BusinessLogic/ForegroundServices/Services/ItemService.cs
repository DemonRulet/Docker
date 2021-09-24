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
        private readonly IItemRepository<TItem> _itemRepository;

        public ItemService(
            IItemRepository<TItem> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public Task<int> GetCount()
        {
            return _itemRepository.Count();
        }

        public Task<int> UpdateCount(TItem item)
        {
            return _itemRepository.UpdateCount(item);
        }
    }
}
