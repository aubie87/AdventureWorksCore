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
        // BusinessEntity data
        [ReadOnly(true)]
        public int Id { get; set; }

        // Person data
        public string Title { get; set; }
        [Required, MinLength(3), DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [Required, MinLength(3), DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Suffix { get; set; }
        //public PersonType PersonType { get; set; }
        [Required, DisplayName("Name Style")]
        public bool NameStyle { get; set; }
        [Required, DisplayName("Email Promotion Level")]
        public int EmailPromotion { get; set; }
        [DisplayName("Person Last Modified")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd hh:mm tt}")]
        public DateTime PersonModifiedDate { get; set; }

        // Employee data
        [ReadOnly(true)]
        [Required, MinLength(8), MaxLength(16), DisplayName("National ID Number")]
        public string NationalIdNumber { get; set; }
        [Required, MinLength(3), MaxLength(256), DisplayName("Login ID")]
        public string LoginId { get; set; }
        [ReadOnly(true), DisplayName("Organization Level"), DefaultValue(0)]
        public short? OrganizationLevel { get; set; }
        [Required, MinLength(3), MaxLength(50), DisplayName("Job Title")]
        public string JobTitle { get; set; }
        [DisplayName("Birthday")]
        [Required, DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required, StringLength(1), DisplayName("Marital Status")]
        public string MaritalStatus { get; set; }
        [Required, StringLength(1)]
        public string Gender { get; set; }

        [DisplayName("Hired Date")]
        [Required, DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
        [Required, DisplayName("Is Salaried"), DefaultValue(true)]
        public bool? SalariedFlag { get; set; }
        [Required, DisplayName("Vacation Hours")]
        public short VacationHours { get; set; }
        [Required, DisplayName("Sick Leave Hours")]
        public short SickLeaveHours { get; set; }
        [Required, DisplayName("Is Current"), DefaultValue(true)]
        public bool? CurrentFlag { get; set; }
        [ReadOnly(true), DisplayName("Emp Last Modified")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss tt}")]
        [DataType(DataType.DateTime)]
        public DateTime EmployeeModifiedDate { get; set; }
    }
}
