using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdWorksCore.Web.Models
{
    public enum PersonType
    {
        //SC = Store Contact
        StoreContact,
        //IN = Individual(retail) customer, 
        Customer,
        //SP = Sales person
        Sales,
        //EM = Employee(non - sales)
        Employee,
        //VC = Vendor contact
        Vendor,
        //GC = General contact
        GeneralContact

    }
}
