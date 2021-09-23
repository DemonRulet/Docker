using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ItemStaticRepository<T> : IItemRepository<T>
    {
        private static ConcurrentBag<T> _items;

        static ItemStaticRepository()
        {
            _items = new ConcurrentBag<T>();
        }

        public Task<int> Add(T items)
        {
            return Task.Run(() => {

                _items.Add(items);
                return 1;
            });
        }

        public Task<IEnumerable<T>> GetAll()
        {
            return Task<IEnumerable<T>>.Factory.StartNew(() => _items);
        }

    }
}


//public class ItemStaticRepository<T> : IItemRepository<T>
//{
//    private static int _count;

//    static ItemStaticRepository()
//    {
//        _count = 0;
//    }

//    public void Add(T items)
//    {
//        _count++;
//    }

//    public int GetCount()
//    {
//        return _count;
//    }
//}