using Microsoft.Extensions.DependencyInjection;
using BusinessLogic.ForegroundServices.Interfaces;
using BusinessLogic.ForegroundServices.Services;

namespace ItemRead.Extensions
{
    public static class AddItemProducerExtenstion
    {
        public static void AddItemProducer(this IServiceCollection service)
        {
            service.AddTransient<IItemProducerService, ItemProduceService>();
        }
    }
}
