using AdWorksCore.HumanResources.Data.Domain;
using AdWorksCore.HumanResources.Data.Entities;
using AdWorksCore.Web.Test.Common;
using AdWorksCore.Web.Views.Employee;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace AdWorksCore.Web.Test.Views.Employee
{
    public class EmployeeControllerShould : IClassFixture<EmployeeMappingFixture>
    {
        private readonly EmployeeMappingFixture map;
        private readonly ITestOutputHelper output;

        public EmployeeControllerShould(EmployeeMappingFixture employeeMappingFixture, ITestOutputHelper output)
        {
            this.map = employeeMappingFixture;
            this.output = output;
        }

        [Fact]
        //[Fact(Skip = "Some reason being skipped.")]
        public void ReturnEmployeeViewModelListForIndex()
        {
            // Arrange
            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(repo => repo.GetEmployees()).Returns(GetTestEmployees());
            var nullLogger = new NullLogger<EmployeeController>();
            var controller = new EmployeeController(mockRepo.Object, map.Mapper, nullLogger);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<EmployeeViewModel>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void ReturnValidEmployeeViewModelForDetail()
        {
            // Arrange
            const int employeeId = 1;
            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(repo => repo.GetEmployee(employeeId))
                .Returns(GetTestEmployees().First(e => e.BusinessEntityId == employeeId));
            var nullLogger = new NullLogger<EmployeeController>();
            var controller = new EmployeeController(mockRepo.Object, map.Mapper, nullLogger);

            // Act
            var result = controller.Detail(employeeId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<EmployeeViewModel>(viewResult.ViewData.Model);
            Assert.Equal(employeeId, model.Id);
        }

        [Fact]
        public void ReturnInvalidEmployeeViewModelForDetail()
        {
            // Arrange
            const int employeeId = 99;
            var mockRepo = new Mock<IEmployeeRepository>();
            mockRepo.Setup(repo => repo.GetEmployee(employeeId)).Returns(GetTestEmployees().FirstOrDefault(e => e.BusinessEntityId == employeeId));
            var nullLogger = new NullLogger<EmployeeController>();
            var controller = new EmployeeController(mockRepo.Object, map.Mapper, nullLogger);

            // Act
            var result = controller.Detail(employeeId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepo.Verify();
        }

        [Fact]
        public void ReturnEmployeeCreateView()
        {
            // Arrange
            var mockRepo = new Mock<IEmployeeRepository>();
            var nullLogger = new NullLogger<EmployeeController>();
            var controller = new EmployeeController(mockRepo.Object, map.Mapper, nullLogger);

            // Act
            var result = controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        private IQueryable<HumanResources.Data.Entities.Employee> GetTestEmployees()
        {
            IList<HumanResources.Data.Entities.Employee> employees = new List<HumanResources.Data.Entities.Employee>()
            {
                new HumanResources.Data.Entities.Employee()
                {
                    BirthDate = DateTime.Now.AddYears(-20).Date,
                    BusinessEntity = new Person()
                    {
                        BusinessEntityId = 1,
                        FirstName = "Abe",
                        LastName = "Adams",
                        Title = "Mr.",
                        NameStyle = true,
                        EmailPromotion = 0,
                        ModifiedDate = DateTime.UtcNow
                    },
                    BusinessEntityId = 1,
                    CurrentFlag = true,
                    Gender = "M",
                    HireDate = DateTime.Now.Date,
                    NationalIdNumber = "555443331",
                    LoginId = "abeadamsadmin",
                    JobTitle = "Administrator",
                    MaritalStatus = "M",
                    SalariedFlag = true,
                    SickLeaveHours = 9,
                    VacationHours = 12
                },
                new HumanResources.Data.Entities.Employee()
                {
                    BirthDate = DateTime.Now.AddYears(-18).Date,
                    BusinessEntity = new Person()
                    {
                        BusinessEntityId = 2,
                        FirstName = "Betty",
                        LastName = "Boop",
                        Title = "Ms.",
                        NameStyle = true,
                        EmailPromotion = 0,
                        ModifiedDate = DateTime.UtcNow
                    },
                    BusinessEntityId = 2,
                    CurrentFlag = true,
                    Gender = "F",
                    HireDate = DateTime.Now.Date,
                    NationalIdNumber = "555443332",
                    LoginId = "abeadamsadmin",
                    JobTitle = "Administrator",
                    MaritalStatus = "S",
                    SalariedFlag = true,
                    SickLeaveHours = 9,
                    VacationHours = 12
                }
            };

            return employees.AsQueryable();
        }
    }
}
