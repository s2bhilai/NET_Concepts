using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_WBC_StorageApp.Repositories
{
    public class GenericRepositoryWithRemove<T,TSuperKey>
        : GenericRepository<T, TSuperKey> 
        where T: Entities.EntityBase,new()
        where TSuperKey: struct
    {
        public void Remove(T item)
        {
            _items.Remove(item);
        }
    }
}
