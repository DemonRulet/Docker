using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IItemRepository<TItem>
    {
        Task<int> UpdateCount(TItem items);
        Task<int> Count();
    }
}
