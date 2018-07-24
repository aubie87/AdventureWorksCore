using AdWorksCore.Web.Views.Employee;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdWorksCore.Web.Test.Views.Employee
{
    public class EmployeeMappingProfileShould
    {
        // testing various instances of mapped objects thru automapper
        private readonly IMapper sut;
        private readonly int businessEntityId = 17;

        public EmployeeMappingProfileShould()
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EmployeeMappingProfile>();
            });

            // Act
            mapConfig.AssertConfigurationIsValid();
            sut = mapConfig.CreateMapper();
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

        [Fact]
        public void MapFromEmployeeToEmployeeViewModel()
        {
            // Arrange
            HumanResources.Data.Entities.Employee employee = GetEmployee();

            // Act
            var employeeViewModel = sut.Map<HumanResources.Data.Entities.Employee, EmployeeViewModel>(employee);

            // Assert
            Assert.Equal(employee.BusinessEntityId, employeeViewModel.Id);
            Assert.Equal(employee.BusinessEntity.BusinessEntityId, employeeViewModel.Id);
            Assert.Equal(employee.BusinessEntity.FirstName, employeeViewModel.FirstName);
            Assert.Equal(employee.ModifiedDate, employeeViewModel.EmployeeModifiedDate);
            Assert.Equal(employee.BusinessEntity.ModifiedDate, employeeViewModel.PersonModifiedDate);
        }
    }
}
