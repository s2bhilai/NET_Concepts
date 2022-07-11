using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_WBC_StorageApp.SpecialCases
{
    public class Container<T>
    {
        public Container() => InstanceCount++;

        public static int InstanceCount { get; private set; }

        public void PrintItem<TItem>(TItem item)
        {
            Console.WriteLine($"Item: {item}");
        }
    }
}
