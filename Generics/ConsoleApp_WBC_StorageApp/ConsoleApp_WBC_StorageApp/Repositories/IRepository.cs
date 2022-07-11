using ConsoleApp_WBC_StorageApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_WBC_StorageApp.Repositories
{

    public interface IWriteRepository<in T>
    {
        void Add(T item);
        void Remove(T item);
        void Save();
    }

    public interface IReadRepository<out T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
    }

    public interface IRepository<T>: IReadRepository<T>,IWriteRepository<T> 
        where T: IEntity
    {    
     
    }

    //Generic interface inheritance
    public interface ISuperRepository<T,TKey> : IRepository<T> where T: IEntity
    {
        TKey key { get; }
    }

    public interface IEmployeeRepository: IRepository<Employee>
    {

    }
}
