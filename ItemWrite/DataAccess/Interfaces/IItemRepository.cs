using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IItemRepository<TItem>
    {
        Task<int> UpdateCount(TItem items);
        Task<int> Count();
    }
}
