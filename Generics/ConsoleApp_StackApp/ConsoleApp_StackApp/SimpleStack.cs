using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_StackApp
{
    public class SimpleStack<T>
    {
        private T[] _items;
        private int currentIndex = -1;

        public SimpleStack() => _items = new T[10];

        public int Count => currentIndex + 1;
  
        public void Push(T item) => _items[++currentIndex] = item;

        public T Pop() => _items[currentIndex--];
    }
}
