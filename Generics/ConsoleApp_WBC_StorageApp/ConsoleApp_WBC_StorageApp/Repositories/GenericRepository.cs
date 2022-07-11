using ConsoleApp_WBC_StorageApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_WBC_StorageApp.Repositories
{

    //Prior to C# 8.0, all reference types were nullable. 
    //Nullable reference types refers to a group of features introduced in
    //C# 8.0 that you can use to minimize the likelihood that 
    //your code causes the runtime to throw System.NullReferenceException

    //We added Entity Base so that we can use the GetById Method 
    // and get the Id from Entity so used constraint otherwise Type T
    // has no such Id property

    //where T: EntityBase then T is a reference type
    //where T: IEntity then T is either a reference type or value type
    // so in a method if it returns null, then compiler will warn that 
    // method may return a non nullable value also
    // so we add a class type 
    //If we add T : class  then T will be a reference type,
    //use struct to make T value type

    //If we use where T: EntityBase, then our class is not loosely coupled,
    // Better to use interface as we can use different types

    //If Nullable is enabled, then T will be non nullable even if we use class
    //constraint, so use where T: class?,IEntity?

    //If we want to create instance of type T using parameter less
    //constructor, use new() contraint, new() should be at the last

    //Suppose Type T (For ex Employee) has parameter constructor
    // then new() will not work

    public class GenericRepository<T,TKey> 
        where T: class,IEntity,new()
        where TKey: struct
    {
        public TKey? Key { get; set; }
        protected readonly List<T> _items = new();

        public T CreateItem()
        {
            return new T();
        }

        public void Add(T item)
        {
            item.Id = _items.Count + 1;
            _items.Add(item);
        }

        public T GetById(int id)
        {
            return _items.Single(item => item.Id == id);

            //return default
            // if value type then returns 0
            // If reference type then returns null
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
