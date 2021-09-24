using DataAccess.Interfaces;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ItemStaticRepository<TItem> : IItemRepository<TItem>
    {
        private static int _count;

        public Task<int> UpdateCount(TItem items)
        {
            return Task.Run(() =>
            {
                _count++;
                return 1;
            });
        }

        public Task<int> Count()
        {
            return Task.Run(() => _count);
        }
    }
}
