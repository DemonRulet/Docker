using System.Threading.Tasks;

namespace BusinessLogic.ForegroundServices.Interfaces
{
    public interface IItemService<TItem>
    {
        Task<int> GetCount(TItem item);
        Task<int> UpdateCount(TItem item);
        Task<int> Add(TItem item);
    }
}
