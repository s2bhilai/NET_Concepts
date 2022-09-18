using AutoMapper;
using EmployeeManagement.Business;
using EmployeeManagement.Controllers;
using EmployeeManagement.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagement.Tests
{
    public class InternalEmployeeControllerTests
    {
        private InternalEmployeesController _internalEmployeesController;
        private InternalEmployee _firstEmployee;

        public InternalEmployeeControllerTests()
        {
            _firstEmployee = new InternalEmployee(
                "Megan", "Jones", 2, 3000, false, 2)
            {
                Id = Guid.Parse("8718e3fb-be08-4471-a99d-bca6e20ec7c9"),
                SuggestedBonus = 400
            };


            //Arrange
            var employeeServiceMock = new Mock<IEmployeeService>();

            employeeServiceMock
                .Setup(m => m.FetchInternalEmployeesAsync())
                .ReturnsAsync(new List<InternalEmployee>()
                {
                    _firstEmployee,
                    //new InternalEmployee("Megan","Jones",2,3000,false,2),
                    new InternalEmployee("Jaimy","Johnson",2,3000,false,2),
                    new InternalEmployee("Anne","Adams",2,3000,false,2)
                });

            //var mapperMock = new Mock<IMapper>();
            //mapperMock.Setup(m =>
            //    m.Map<InternalEmployee, Models.InternalEmployeeDto>
            //    (It.IsAny<InternalEmployee>()))
            //    .Returns(new Models.InternalEmployeeDto());
            //Use an actual mapper instance instead of Mock
            var mapperConfiguration = new MapperConfiguration(
                cfg => cfg.AddProfile<MapperProfiles.EmployeeProfile>());
            var mapper = new Mapper(mapperConfiguration);



            _internalEmployeesController =
                new InternalEmployeesController(
                    employeeServiceMock.Object, mapper);
        }


        [Fact]
        public async Task GetInternalEmployees_GetAction_MustReturnOkObjectResult()
        {
            //Act
            var result = await _internalEmployeesController.GetInternalEmployees();

            //Assert
            var actionResult = Assert
                .IsType<ActionResult<IEnumerable<Models.InternalEmployeeDto>>>(result);

            Assert.IsType<OkObjectResult>(actionResult.Result);

        }

        [Fact]
        public async Task GetInternalEmployees_GetAction_MustReturnIEnumerableOfInternalEmployeeDtoAsModelType()
        {
            //Arrange

            //Act
            var result = await _internalEmployeesController.GetInternalEmployees();

            //Assert
            //Assert.IsType will not work in interfaces
            var actionResult = Assert
                .IsType<ActionResult<IEnumerable<Models.InternalEmployeeDto>>>(result);

            Assert.IsAssignableFrom<IEnumerable<Models.InternalEmployeeDto>>(
                ((OkObjectResult)actionResult.Result).Value);

        }

        [Fact]
        public async Task GetInternalEmployees_GetAction_MustReturnNumberOfInputtedReturnEmployees()
        {
            //Arrange

            //Act
            var result = await _internalEmployeesController.GetInternalEmployees();

            //Assert
            var actionResult = Assert
               .IsType<ActionResult<IEnumerable<Models.InternalEmployeeDto>>>(result);

            Assert.Equal(3,
                ((IEnumerable<Models.InternalEmployeeDto>)
                ((OkObjectResult)actionResult.Result).Value).Count());
        }

        [Fact]
        public async Task GetInternalEmployees_GetAction_ReturnOkObjectResultWithCorrectAmountOfInternalEmployees()
        {
            //Arrange

            //Act
            var result = await _internalEmployeesController.GetInternalEmployees();

            //Assert
            var actionResult = Assert
               .IsType<ActionResult<IEnumerable<Models.InternalEmployeeDto>>>(result);

            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            var dtos = Assert.IsAssignableFrom<IEnumerable<Models.InternalEmployeeDto>>
                (okObjectResult.Value);

            Assert.Equal(3, dtos.Count());

            var firstEmployee = dtos.First();
            Assert.Equal(_firstEmployee.Id, firstEmployee.Id);
            Assert.Equal(_firstEmployee.FirstName, firstEmployee.FirstName);
            Assert.Equal(_firstEmployee.SuggestedBonus, firstEmployee.SuggestedBonus);
        }

    }
}
