using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagement.Tests
{
    public class EmployeeFactoryTests: IDisposable
    {
        private EmployeeFactory _employeeFactory;
        public EmployeeFactoryTests()
        {
            _employeeFactory = new EmployeeFactory();
        }

        public void Dispose()
        {
            //clean up the set up code, if required
        }

        [Fact(Skip = "Skipping this one for demo reasons.")]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500()
        {
           
            var employee = (InternalEmployee)_employeeFactory
                    .CreateEmployee("Subin", "S");

            Assert.Equal(2500, employee.Salary);
        }

        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500()
        {
            

            var employee = (InternalEmployee)_employeeFactory
                    .CreateEmployee("Subin", "S");

            Assert.True(employee.Salary >= 2500,"Salry not expected range");
            Assert.True(employee.Salary <= 3500);

        }

        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500_AlternativeInRange()
        {
       

            var employee = (InternalEmployee)_employeeFactory
                    .CreateEmployee("Subin", "S");

            Assert.InRange(employee.Salary, 2500, 3500);

        }

        [Fact]
        [Trait("Category", "EmployeeFactory_CreateEmployee_Salary")]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500_PrecisionExample()
        {
          

            var employee = (InternalEmployee)_employeeFactory
                    .CreateEmployee("Subin", "S");
            employee.Salary = 2500.123m;

            Assert.Equal(2500,employee.Salary,0);

        }

        [Fact]
        [Trait("Category","EmployeeFactory_CreateEmployee_ReturnType")]
        public void CreateEmployee_IsExternalIsTrue_ReturnTypeMustBeExternalEmployee()
        {
            //Arrange
            //var factory = new EmployeeFactory();

            //ACt
            var employee = _employeeFactory.CreateEmployee("Subin", "s", "QR", true);

            //Assert
            Assert.IsType<ExternalEmployee>(employee);

            Assert.IsAssignableFrom<Employee>(employee);
        }
    }
}
