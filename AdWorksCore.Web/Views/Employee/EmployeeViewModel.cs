using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AdWorksCore.HumanResources.Data.Entities;
using AdWorksCore.Web.Models;

namespace AdWorksCore.Web.Views.Employee
{
    public class EmployeeViewModel
    {
        public static EmployeeViewModel FromPerson(Person person)
        {
            return new EmployeeViewModel()
            {
                Id = person.BusinessEntityId,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName,
                Title = person.Title,
                Suffix = person.Suffix,
                LastModified = person.ModifiedDate.ToLocalTime()
            };
        }

        public static IList<EmployeeViewModel> FromPerson(IList<Person> employeeList)
        {
            List<EmployeeViewModel> vmList = new List<EmployeeViewModel>();
            foreach(var p in employeeList)
            {
                vmList.Add(EmployeeViewModel.FromPerson(p));
            }
            return vmList;
        }

        public void CopyToPerson(Person storedEmp)
        {
            storedEmp.FirstName = FirstName;
            storedEmp.LastName = LastName;
            storedEmp.MiddleName = MiddleName;
            storedEmp.Suffix = Suffix;
            storedEmp.Title = Title;
            storedEmp.ModifiedDate = DateTime.UtcNow;
        }

        [ReadOnly(true)]
        public int Id { get; set; }
        public string Title { get; set; }
        [Required, MinLength(3), DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [Required, MinLength(3), DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public PersonType PersonType { get; set; }
        [DisplayName("Last Modified")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd hh:mm tt}")]
        public DateTime LastModified { get; set; }
    }
}
