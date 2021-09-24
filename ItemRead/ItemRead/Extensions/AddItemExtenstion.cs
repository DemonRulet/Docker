using Microsoft.Extensions.DependencyInjection;
using BusinessLogic.ForegroundServices.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using BusinessLogic.ForegroundServices.Services;

namespace ItemRead.Extensions
{
    public static class AddItemExtenstion
    {
        public static void AddItem<TItem>(this IServiceCollection service)
        {
            service.AddTransient<IItemRepository<TItem>, ItemStaticRepository<TItem>>();
            service.AddTransient<IItemService<TItem>, ItemService<TItem>>();
        }
    }
}
