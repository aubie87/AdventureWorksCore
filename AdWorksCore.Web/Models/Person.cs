using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdWorksCore.Web.Models
{
    /* Primary type of person: 
     *  SC = Store Contact, 
     *  IN = Individual(retail) customer, 
     *  SP = Sales person, 
     *  EM = Employee(non - sales), 
     *  VC = Vendor contact, 
     *  GC = General contact
     */
    public class Person
    {
        [ReadOnly(true)]
        public int Id { get; set; }
        public string PersonType { get; set; }
        public string Title { get; set; }
        [Required, MinLength(3), DisplayName("First Name")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required, MinLength(3)]
        public string LastName { get; set; }
        public string Suffix { get; set; }
        [ReadOnly(true)]
        public DateTime LastModified { get; set; }
    }
}
