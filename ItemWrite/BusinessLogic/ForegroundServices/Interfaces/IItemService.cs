using System.Threading.Tasks;

namespace BusinessLogic.ForegroundServices.Interfaces
{
    public interface IItemService<TItem>
    {
        Task<int> GetCount();
        Task<int> UpdateCount(TItem item);
        Task<int> Add(TItem item);
    }
}
