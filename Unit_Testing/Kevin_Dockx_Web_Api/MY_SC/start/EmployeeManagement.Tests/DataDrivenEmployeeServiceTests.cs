using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Tests.Fixtures;
using EmployeeManagement.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagement.Tests
{
    [Collection("EmployeeServiceCollection")]
    public class DataDrivenEmployeeServiceTests //: IClassFixture<EmployeeServiceFixture>
    {
        private EmployeeServiceFixture _employeeServiceFixture;

        public DataDrivenEmployeeServiceTests(EmployeeServiceFixture employeeServiceFixture)
        {
            _employeeServiceFixture = employeeServiceFixture;
        }

        [Fact]
        public async Task GiveRaise_MinimumRaiseGiven_EmployeeminimumRaiseGivenMustBeTrue()
        {
            //Arrange
            var internalEmployee = new InternalEmployee(
                "Brooklyn", "Cannon", 5, 3000, false, 1);

            //Act
            await _employeeServiceFixture
                .EmployeeService.GiveRaiseAsync(internalEmployee, 100);

            //Assert
            Assert.True(internalEmployee.MinimumRaiseGiven);
        }

        [Fact]
        public async Task GiveRaise_MoreThanMinimumRaiseGiven_EmployeeminimumRaiseGivenMustBeTrue()
        {
            //Arrange
            var internalEmployee = new InternalEmployee(
                "Brooklyn", "Cannon", 5, 3000, false, 1);

            //Act
            await _employeeServiceFixture
                .EmployeeService.GiveRaiseAsync(internalEmployee, 200);

            //Assert
            Assert.False(internalEmployee.MinimumRaiseGiven);
        }

        public static IEnumerable<object[]> ExampleTestDataForGiveRaise_WithProperty
        {
            get
            {
                return new List<object[]>
                {
                    new object[] {100,true},
                    new object[] {200,false}
                };
            }
        }

        public static IEnumerable<object[]> ExampleTestDataForGiveRaise_WithMethod(
            int testDataInstancesToProvide)
        {            
            var testData =  new List<object[]>
            {
                new object[] {100,true},
                new object[] {200,false}
            };

            return testData.Take(testDataInstancesToProvide);
           
        }

        [Theory]
        //[MemberData(nameof(ExampleTestDataForGiveRaise_WithMethod),1)]
        //[ClassData(typeof(EmployeeServiceTestData))]
        //[ClassData(typeof(StronglyTypedEmployeeServiceTestData))]
        [ClassData(typeof(StronglyTypedEmployeeServiceTestData_FromFile))]
        public async Task GiveRaise_RaiseGiven_EmployeeMinimumRaiseGivenMatchesValue(
            int raiseGiven,bool expectedValueForMinimumRaiseGiven)
        {
            //Arrange
            var internalEmployee = new InternalEmployee(
                "Brooklyn", "Cannon", 5, 3000, false, 1);

            //Act
            await _employeeServiceFixture
                .EmployeeService.GiveRaiseAsync(internalEmployee, raiseGiven);

            //Assert
            Assert.Equal(expectedValueForMinimumRaiseGiven,internalEmployee.MinimumRaiseGiven);
        }

        [Theory]
        [InlineData("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e")]
        [InlineData("37e03ca7-c730-4351-834c-b66f280cdb01")]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse
            (Guid courseId)
        {
            //Arrange


            //act
            var internalEmployee = _employeeServiceFixture
                .EmployeeService
                .CreateInternalEmployee("Brooklyn", "Cannon");

            //Assert
            Assert.Contains(internalEmployee.AttendedCourses,
                course => course.Id == courseId);
        }   
    }
}
