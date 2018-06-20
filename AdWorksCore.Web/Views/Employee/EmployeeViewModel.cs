using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AdWorksCore.Web.Models;

namespace AdWorksCore.Web.Views.Employee
{
    public class EmployeeViewModel
    {
        public static EmployeeViewModel FromPerson(Person person)
        {
            return new EmployeeViewModel()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName,
                Title = person.Title,
                Suffix = person.Suffix
            };
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
    }
}
