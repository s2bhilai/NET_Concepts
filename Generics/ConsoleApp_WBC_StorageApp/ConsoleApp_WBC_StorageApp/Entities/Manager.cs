using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_WBC_StorageApp.Entities
{
    public class Manager:Employee
    {
        public override string ToString() => base.ToString() + " (Manager)";
    }
}
