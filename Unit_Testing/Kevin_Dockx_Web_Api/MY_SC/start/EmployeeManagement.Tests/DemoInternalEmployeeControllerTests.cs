using AutoMapper;
using EmployeeManagement.Business;
using EmployeeManagement.Controllers;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagement.Tests
{
    public class DemoInternalEmployeeControllerTests
    {
        [Fact]
        public async Task CreateInternalEmployee_InvalidInput_MustReturnBadRequest()
        {
            //Arrange
            var employeeServiceMock = new Mock<IEmployeeService>();
            var mapperMock = new Mock<IMapper>();

            var demoInternalEmployeesController =
                new DemoInternalEmployeeController(
                    employeeServiceMock.Object, mapperMock.Object);

            var internalEmployeeForCreationDto =
                new InternalEmployeeForCreationDto();

            demoInternalEmployeesController.ModelState
                .AddModelError("FirstName", "Required");

            //Act
            var result = await demoInternalEmployeesController
                .CreateInternalEmployee(internalEmployeeForCreationDto);

            //Assert
            var actionResult = Assert
                .IsType<ActionResult<Models.InternalEmployeeDto>>(result);

            var badObjectResult = 
                Assert.IsType<BadRequestObjectResult>(actionResult.Result);

            Assert.IsType<SerializableError>(badObjectResult.Value);
        }

        [Fact]
        public void GetProtectedInternalEmployee_GetActionForUserInAdminRole_MustRedirect()
        {
            //Arrange
            var employeeServiceMock = new Mock<IEmployeeService>();
            var mapperMock = new Mock<IMapper>();

            var demoInternalEmployeesController =
                new DemoInternalEmployeeController(
                    employeeServiceMock.Object, mapperMock.Object);

            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Karen"),
                new Claim(ClaimTypes.Role,"Admin")
            };

            var claimsIdentity = new ClaimsIdentity(userClaims, "UnitTest");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var httpContext = new DefaultHttpContext()
            {
                User = claimsPrincipal
            };

            demoInternalEmployeesController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            //ACt
            var result = demoInternalEmployeesController.GetProtectedInternalEmployees();

            //Assert
            var actionResult = Assert.IsAssignableFrom<IActionResult>(result);
            var redirectToActionResut = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("GetInternalEmployees", redirectToActionResut.ActionName);
            Assert.Equal("ProtectedInternalEmployees", redirectToActionResut.ControllerName);

        }

        [Fact]
        public void GetProtectedInternalEmployee_GetActionForUserInAdminRole_MustRedirect_WithMoq()
        {
            // Arrange
            var employeeServiceMock = new Mock<IEmployeeService>();
            var mapperMock = new Mock<IMapper>();

            var demoInternalEmployeesController =
                new DemoInternalEmployeeController(
                    employeeServiceMock.Object, mapperMock.Object);

            var mockPrincipal = new Mock<ClaimsPrincipal>();
            mockPrincipal.Setup(x =>
                x.IsInRole(It.Is<string>(s => s == "Admin")))
                .Returns(true);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(c => c.User)
                .Returns(mockPrincipal.Object);

            demoInternalEmployeesController.ControllerContext =
                new ControllerContext()
                {
                    HttpContext = httpContextMock.Object
                };

            //ACt
            var result = demoInternalEmployeesController.GetProtectedInternalEmployees();

            //Assert
            var actionResult = Assert.IsAssignableFrom<IActionResult>(result);
            var redirectToActionResut = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("GetInternalEmployees", redirectToActionResut.ActionName);
            Assert.Equal("ProtectedInternalEmployees", redirectToActionResut.ControllerName);

        }

    }
}
