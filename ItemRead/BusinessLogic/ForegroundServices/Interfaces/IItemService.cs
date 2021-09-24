using System.Threading.Tasks;

namespace BusinessLogic.ForegroundServices.Interfaces
{
    public interface IItemService<TItem>
    {
        Task<int> Get(TItem item);
        Task<int> Add(TItem item);
    }
}
