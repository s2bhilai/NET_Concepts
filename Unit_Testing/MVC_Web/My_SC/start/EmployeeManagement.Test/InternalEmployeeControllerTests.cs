using AutoMapper;
using EmployeeManagement.Business;
using EmployeeManagement.Controllers;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagement.Test
{
    public class InternalEmployeeControllerTests
    {
        [Fact]
        public async Task AddInternalEmployee_InvalidInput_MustReturnBadRequest()
        {
            //Arrange
            var employeeServiceMock = new Mock<IEmployeeService>();
            var mapperMock = new Mock<IMapper>();
            var internalEmployeeController = new InternalEmployeeController(
                employeeServiceMock.Object, mapperMock.Object);

            var createInternalEmployeeViewModel = new
                CreateInternalEmployeeViewModel();

            internalEmployeeController.ModelState
                .AddModelError("FirstName", "Required");

            //Act
            var result = await internalEmployeeController
                .AddInternalEmployee(createInternalEmployeeViewModel);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task InternalEmployeeDetails_InputFromTempData_MustReturnCorrectEmployee()
        {
            var expectedEmployeeId =
                Guid.Parse("72f2f5fe-e50c-4966-8420-d50258aefdcb");

            var employeeServiceMock = new Mock<IEmployeeService>();
            employeeServiceMock
                .Setup(m => m.FetchInternalEmployeeAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new DataAccess.Entities.InternalEmployee("Jaimy", "Johnson", 3, 3400, true, 1)
                    {
                        Id = expectedEmployeeId,
                        SuggestedBonus = 500
                    });

            var mapperConfiguration = new MapperConfiguration(
                cfg => cfg.AddProfile<MapperProfiles.EmployeeProfile>());

            var mapper = new Mapper(mapperConfiguration);

            var internalEmployeeController = new
                InternalEmployeeController(employeeServiceMock.Object,mapper);

            var tempDataDictionary = new TempDataDictionary(
                new DefaultHttpContext(),
                new Mock<ITempDataProvider>().Object);

            tempDataDictionary["EmployeeId"] = expectedEmployeeId;

            internalEmployeeController.TempData = tempDataDictionary;

            //Act
            var result = await internalEmployeeController
                .InternalEmployeeDetails(null);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsType<InternalEmployeeDetailViewModel>
                (viewResult.Model);

            Assert.Equal(expectedEmployeeId, viewModel.Id);

        }

        [Fact]
        public async Task InternalEmployeeDetails_InputFromSession_MustReturnCorrectEmployee()
        {
            //Arrange
            var expectedEmployeeId =
               Guid.Parse("72f2f5fe-e50c-4966-8420-d50258aefdcb");

            var employeeServiceMock = new Mock<IEmployeeService>();
            employeeServiceMock
                .Setup(m => m.FetchInternalEmployeeAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new DataAccess.Entities.InternalEmployee("Jaimy", "Johnson", 3, 3400, true, 1)
                    {
                        Id = expectedEmployeeId,
                        SuggestedBonus = 500
                    });

            var mapperConfiguration = new MapperConfiguration(
                cfg => cfg.AddProfile<MapperProfiles.EmployeeProfile>());

            var mapper = new Mapper(mapperConfiguration);

            var internalEmployeeController = new
                InternalEmployeeController(employeeServiceMock.Object, mapper);

            var defaultHttpContext = new DefaultHttpContext();

            var sessionMock = new Mock<ISession>();
            var guidAsBytes = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
            sessionMock.Setup(s => s.TryGetValue(It.IsAny<string>(), out guidAsBytes))
                .Returns(true);

            defaultHttpContext.Session = sessionMock.Object;

            internalEmployeeController.ControllerContext = new ControllerContext
            {
                HttpContext = defaultHttpContext
            };

            //Act
            var result = await internalEmployeeController
                .InternalEmployeeDetails(null);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert
                .IsType<InternalEmployeeDetailViewModel>(viewResult.Model);

            Assert.Equal(expectedEmployeeId, viewModel.Id);


        }


    }
}
