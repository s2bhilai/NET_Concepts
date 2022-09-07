using EmployeeManagement.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagement.Tests
{
    public class EmployeeTests
    {
        [Fact]
        public void EmployeeFullNamePropertyGetter_InputFirstAndLastName_FullNameIsConcatenation()
        {
            //Arrange
            var employee = new InternalEmployee("Subin", "S", 0, 2500, false, 1);

            //Act
            employee.FirstName = "Chitra";
            employee.LastName = "Suresh";

            //Assert
            Assert.Equal("Chitra Suresh", employee.FullName,ignoreCase: true);
        }

        [Fact]
        public void EmployeeFullNamePropertyGetter_InputFirstAndLastName_FullNameStartsWithFirstName()
        {
            //Arrange
            var employee = new InternalEmployee("Subin", "S", 0, 2500, false, 1);

            //Act
            employee.FirstName = "Chitra";
            employee.LastName = "Suresh";

            //Assert
            Assert.StartsWith(employee.FirstName, employee.FullName);
        }

        [Fact]
        public void EmployeeFullNamePropertyGetter_InputFirstAndLastName_FullNameEndsWithLastName()
        {
            //Arrange
            var employee = new InternalEmployee("Subin", "S", 0, 2500, false, 1);

            //Act
            employee.FirstName = "Chitra";
            employee.LastName = "Suresh";

            //Assert
            Assert.EndsWith(employee.LastName, employee.FullName);
        }

        [Fact]
        public void EmployeeFullNamePropertyGetter_InputFirstAndLastName_FullNameContainsPartOfConcatenation()
        {
            //Arrange
            var employee = new InternalEmployee("Subin", "S", 0, 2500, false, 1);

            //Act
            employee.FirstName = "Chitra";
            employee.LastName = "Suresh";

            //Assert
            Assert.DoesNotContain("dfdf", employee.FullName);
        }
    }
}
