using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Services;
using EmployeeManagement.Services.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Tests.Fixtures
{
    public class EmployeeServiceFixture:IDisposable
    {
        public IEmployeeManagementRepository EmployeeManagementTestDataRepository
        { get;  }

        public EmployeeService EmployeeService
        { get; }

        public EmployeeServiceFixture()
        {
            EmployeeManagementTestDataRepository =
                new EmployeeManagementTestDataRepository();

            EmployeeService = new EmployeeService(
                EmployeeManagementTestDataRepository, new EmployeeFactory());
        }

        public void Dispose()
        {
            //Clean up set up code if required
        }
    }
}
