using ConsoleApp_WBC_StorageApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_WBC_StorageApp.Repositories
{

    //public delegate void ItemAdded(object item);
    //We can use Type T as Contravariant only if it's not used as a Return Type
    //public delegate T ItemAdded<in T>(T item); --> Wrong
    // If it's used both as input and output then define it as invariant
    //public delegate T ItemAdded<T>(T item); --> Invariant
    //If T is used only as output then used as a Co-Variant
    //public delegate T ItemAdded<out T>(); --> Co-Variant
    public delegate void ItemAdded<in T>(T item);


    //public delegate void Action<in T>(T arg); - ContraVariant
    //public delegate TResult Func<out TResult>(); - CoVariant
    //public delegate TResult Func<in T,out TResult>(T arg);

    public class SqlRepository<T> : IRepository<T> where T: class, IEntity
    {
        private DbContext _dbContext;
        private DbSet<T> _dbSet;
        private ItemAdded<T>? _itemAddedCallback;

        public SqlRepository(DbContext dbContext,ItemAdded<T>? itemAddedCallback = null)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
            _itemAddedCallback = itemAddedCallback;
        }
        
        public void Add(T item)
        {
            _dbSet.Add(item);
            _itemAddedCallback?.Invoke(item);
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
