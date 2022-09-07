using FactoryPattern.AbstractFactory;
using System;

namespace FactoryPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            new NavigationBar(new Android());
            new DropdownMenu(new Apple());
        }
    }
}
