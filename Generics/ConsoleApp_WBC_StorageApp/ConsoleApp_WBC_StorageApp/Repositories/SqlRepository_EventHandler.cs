using ConsoleApp_WBC_StorageApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_WBC_StorageApp.Repositories
{
    public class SqlRepository_EventHandler<T> :
                IRepository<T> where T : class, IEntity
    {
        private DbContext _dbContext;
        private DbSet<T> _dbSet;

        public SqlRepository_EventHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        //public event Action<T>? ItemAdded;
        public event EventHandler<T> ItemAdded;

        public void Add(T item)
        {
            _dbSet.Add(item);
            ItemAdded?.Invoke(this,item);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.OrderBy(i => i.Id).ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);

            //return default
            // if value type then returns 0
            // If reference type then returns null
        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
