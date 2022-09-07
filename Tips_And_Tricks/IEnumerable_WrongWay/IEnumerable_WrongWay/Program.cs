using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IEnumerable_WrongWay
{
    class Program
    {
        static void Main(string[] args)
        {
            var customers = GetCustomers_Solution();

            var count = customers.Count();
            Console.WriteLine($"There are {count} customers");

            foreach (var customer in customers)
            {
                Console.WriteLine(customer.FullName);
            }

            Console.ReadLine();
        }

        //Multiple enumerations of the enumerable
        //Advantage here is lazy loaded or load on demand since we are not calling ToList
        static IEnumerable<Customer> GetCustomers()
        {
            var lines = File.ReadAllLines("./Customers.csv");

            return lines.Select(x =>
            {
                var splitline = x.Split(',');
                return new Customer(splitline[0], int.Parse(splitline[1]));
            });

            //foreach (var line in lines)
            //{
            //    var splitLine = line.Split(',');
            //    yield return new Customer(splitLine[0], int.Parse(splitLine[1]));
            //}
        }

        static IEnumerable<Customer> GetCustomers_Solution()
        {
            var list = new List<Customer>();
            var lines = File.ReadAllLines("./Customers.csv");

            //return lines.Select(x =>
            //{
            //    var splitline = x.Split(',');
            //    return new Customer(splitline[0], int.Parse(splitline[1]));
            //});

            foreach (var line in lines)
            {
                var splitLine = line.Split(',');
                list.Add(new Customer(splitLine[0],int.Parse(splitLine[1])));
            }

            return list;
        }
    }

    record Customer(string FullName,int Age);
}
