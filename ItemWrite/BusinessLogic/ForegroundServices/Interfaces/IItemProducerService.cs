using System.Threading.Tasks;

namespace BusinessLogic.ForegroundServices.Interfaces
{
    public interface IItemProducerService
    {
        Task<int> SendToKafka<TItem>(TItem item);
    }
}
