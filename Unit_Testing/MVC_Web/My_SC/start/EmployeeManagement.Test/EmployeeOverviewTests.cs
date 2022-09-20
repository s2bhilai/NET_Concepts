using AutoMapper;
using EmployeeManagement.Business;
using EmployeeManagement.Controllers;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagement.Test
{
    public class EmployeeOverviewTests
    {
        private EmployeeOverviewController employeeOverviewController;

        public EmployeeOverviewTests()
        {
            var employeeServiceMock = new Mock<IEmployeeService>();

            employeeServiceMock
                .Setup(m => m.FetchInternalEmployeesAsync())
                .ReturnsAsync(new List<InternalEmployee>()
                {
                    new InternalEmployee("Megan","Jones",2,3000,false,2),
                    new InternalEmployee("Jaimy","Johnson",2,3000,false,2),
                    new InternalEmployee("Anne","Adams",2,3000,false,2)
                });

            //For unit tests, dont use the mock of mapper instead create
            // mapper instance

            var mapperConfiguration = new MapperConfiguration(
                cfg => cfg.AddProfile<MapperProfiles.EmployeeProfile>());
            var mapper = new Mapper(mapperConfiguration);

            employeeOverviewController = new EmployeeOverviewController(
                employeeServiceMock.Object, mapper);
        }


        [Fact]
        public async Task Index_GetAction_MustReturnViewResult()
        {
            //Arrange
           

            //ACt
            var result = await employeeOverviewController.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Index_GetAction_MustReturnEmployeeOverviewViewModelAsViewModelType()
        {
            //Arrange

            //Act
            var result = await employeeOverviewController.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<EmployeeOverviewViewModel>(viewResult.Model);

            //other option
            Assert.IsType<EmployeeOverviewViewModel>(
                ((ViewResult)result).Model);

        }

        [Fact]
        public async Task Index_GetAction_MustReturnNumberOfInputtedInternalEmployees()
        {
            //Arrange

            //Act
            var result = await employeeOverviewController.Index();

            //Assert
            Assert.Equal(3,
                ((EmployeeOverviewViewModel)((ViewResult)result).Model)
                .InternalEmployees.Count);
                
        }

        [Fact]
        public async Task Index_GetAction_ReturnsViewResultWithInternalEmployees()
        {
            //Ararnge

            //Act
            var result = await employeeOverviewController.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsType<EmployeeOverviewViewModel>(viewResult.Model);
            Assert.Equal(3, viewModel.InternalEmployees.Count());
        }
    }
}
