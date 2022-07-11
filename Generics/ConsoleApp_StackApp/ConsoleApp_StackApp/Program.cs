using System;

namespace ConsoleApp_StackApp
{
    class Program
    {

        //Advantages
        //1. Code-Reuse
        //2. Type-safety
        //3. Performance (no boxing/unboxing)



        static void Main(string[] args)
        {
            StackDoubles();

            StackStrings();

            Console.ReadLine();
        }

        private static void StackDoubles()
        {
            var stack = new SimpleStack<double>();
            stack.Push(2.3);
            stack.Push(2.5);
            stack.Push(2.8);

            double sum = 0.0;

            while (stack.Count > 0)
            {
                double item = (double)stack.Pop();
                Console.WriteLine($"item: {item}");
                sum += item;
            }

            Console.WriteLine($"Sum: {sum}");
            Console.ReadLine();
        }

        private static void StackStrings()
        {
            var stack = new SimpleStack<string>();
            stack.Push("Wired Brain Coffee");
            stack.Push("test");

            while(stack.Count > 0)
            {
                Console.WriteLine(stack.Pop());
            }
        }
    }
}
