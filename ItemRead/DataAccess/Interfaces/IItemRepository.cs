using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IItemRepository<TItem>
    {
        Task<int> Add(TItem items);
    }
}
