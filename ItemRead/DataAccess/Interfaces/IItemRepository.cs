using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IItemRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<int> Add(T items);
    }
}
