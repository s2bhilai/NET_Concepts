using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp_WBC_StorageApp.Entities
{
    public static class EntityExtensions
    {
        //where T: IEntity so that it's applied only to Entities otherwise applied to all 
        // the objects

        public static T? Copy<T>(this T itemToCopy) where T: IEntity
        {
            var json = JsonSerializer.Serialize<T>(itemToCopy);

            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
