using System.Threading.Tasks;

namespace BusinessLogic.ForegroundServices.Interfaces
{
    public interface IItemServiceKafka
    {
        Task<int> Push<TItem>(TItem item);
    }
}
