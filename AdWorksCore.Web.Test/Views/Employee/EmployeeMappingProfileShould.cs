using AdWorksCore.Web.Test.Common;
using AdWorksCore.Web.Views.Employee;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace AdWorksCore.Web.Test.Views.Employee
{
    public class EmployeeMappingProfileShould
    {
        // testing various instances of mapped objects thru automapper
        private readonly IMapper sut;
        private readonly int businessEntityId = 17;
        private readonly ITestOutputHelper output;

        public EmployeeMappingProfileShould(ITestOutputHelper output)
        {
            this.output = output;
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EmployeeMappingProfile>();
            });

            // Act
            mapConfig.AssertConfigurationIsValid();
            sut = mapConfig.CreateMapper();
            output.WriteLine("Created mapping and correctly asserted the configuration.");
        }

        [Fact]
        public void MapFromEmployeeToEmployeeViewModel()
        {
            // Arrange
            HumanResources.Data.Entities.Employee employee = GetEmployee();

            // Act
            var employeeViewModel = sut.Map<HumanResources.Data.Entities.Employee, EmployeeViewModel>(employee);

            // Assert
            Assert.IsType<EmployeeViewModel>(employeeViewModel);
            Assert.Equal(employee.BusinessEntityId, employeeViewModel.Id);
            Assert.Equal(employee.BusinessEntity.BusinessEntityId, employeeViewModel.Id);
            Assert.Equal(employee.BusinessEntity.FirstName, employeeViewModel.FirstName);
            Assert.Equal(employee.ModifiedDate, employeeViewModel.EmployeeModifiedDate);
            Assert.Equal(employee.BusinessEntity.ModifiedDate, employeeViewModel.PersonModifiedDate);
        }

        [Fact]
        public void MapFromViewModelToEmployee()
        {
            EmployeeViewModel employeeViewModel = GetEmployeeViewModel();

            var employee = sut.Map<EmployeeViewModel, HumanResources.Data.Entities.Employee>(employeeViewModel);

            Assert.IsType<HumanResources.Data.Entities.Employee>(employee);
            Assert.Equal(employeeViewModel.Id, employee.BusinessEntityId);
            Assert.Equal(employeeViewModel.Id, employee.BusinessEntity.BusinessEntityId);
            Assert.Equal(employeeViewModel.PersonModifiedDate, employee.BusinessEntity.ModifiedDate);
            Assert.Equal(employeeViewModel.EmployeeModifiedDate, employee.ModifiedDate);
            Assert.Equal(employeeViewModel.FirstName, employee.BusinessEntity.FirstName);
            Assert.Equal(employeeViewModel.LastName, employee.BusinessEntity.LastName);
        }

        private EmployeeViewModel GetEmployeeViewModel()
        {
            return new EmployeeViewModel()
            {
                Id = 9,
                FirstName = "Curt",
                LastName = "Cowan",
                Title = "Mr.",
                NameStyle = true,
                EmailPromotion = 0,
                EmployeeModifiedDate = DateTime.UtcNow,
                PersonModifiedDate = DateTime.UtcNow.AddDays(-1),
                BirthDate = DateTime.Now.AddYears(-22).Date,
                CurrentFlag = true,
                Gender = "M",
                HireDate = DateTime.Now.Date,
                NationalIdNumber = "555443333",
                LoginId = "abeadamsadmin",
                JobTitle = "Administrator",
                MaritalStatus = "M",
                SalariedFlag = true,
                SickLeaveHours = 9,
                VacationHours = 12
            };
        }

        private HumanResources.Data.Entities.Employee GetEmployee()
        {
            return new HumanResources.Data.Entities.Employee()
            {
                BirthDate = DateTime.Now.AddYears(-20).Date,
                BusinessEntity = new HumanResources.Data.Entities.Person()
                {
                    BusinessEntityId = businessEntityId,
                    FirstName = "Abe",
                    LastName = "Adams",
                    Title = "Mr.",
                    NameStyle = true,
                    EmailPromotion = 0,
                    ModifiedDate = DateTime.UtcNow
                },
                BusinessEntityId = businessEntityId,
                CurrentFlag = true,
                Gender = "M",
                HireDate = DateTime.Now.Date,
                NationalIdNumber = "555443333",
                LoginId = "abeadamsadmin",
                JobTitle = "Administrator",
                MaritalStatus = "M",
                SalariedFlag = true,
                SickLeaveHours = 9,
                VacationHours = 12
            };
        }
    }
}
