using ConsoleApp_WBC_StorageApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_WBC_StorageApp.Repositories
{
    public class ListRepository<T>: IRepository<T> where T: IEntity
    {
        protected readonly List<T> _items = new();

        public void Add(T item)
        {
            item.Id = _items.Count + 1;
            _items.Add(item);
        }

        public IEnumerable<T> GetAll()
        {
            return _items.ToList();
        }

        public T GetById(int id)
        {
            return _items.Single(item => item.Id == id);

            //return default
            // if value type then returns 0
            // If reference type then returns null
        }

        public void Remove(T item)
        {
            //_dbSet.Remove(item);
            _items.Remove(item);
        }

        public void Save()
        {
            foreach (var item in _items)
            {
                Console.WriteLine(item);
            }
        }
    }
}
