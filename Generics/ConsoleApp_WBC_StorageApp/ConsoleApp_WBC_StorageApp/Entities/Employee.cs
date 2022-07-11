using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_WBC_StorageApp.Entities
{
    public class Employee:EntityBase
    {
        //public Employee(string firstName)
        //{

        //}
        //public int Id { get; set; }
        public string? FirstName { get; set; }

        public override string ToString() => $"Id: {Id}, FirstName: {FirstName}";
    }
}
