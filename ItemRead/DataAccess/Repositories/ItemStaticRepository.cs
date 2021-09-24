using DataAccess.Interfaces;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ItemStaticRepository<TItem> : IItemRepository<TItem>
    {
        private static ConcurrentBag<TItem> _items;

        static ItemStaticRepository()
        {
            _items = new ConcurrentBag<TItem>();
        }

        public Task<int> Add(TItem items)
        {
            return Task.Run(() => {

                _items.Add(items);
                return 1;
            });
        }
    }
}
