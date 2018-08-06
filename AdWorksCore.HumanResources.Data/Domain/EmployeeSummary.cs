using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdWorksCore.HumanResources.Data.Domain
{
    public class EmployeeSummary
    {
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string JobTitle { get; internal set; }
    }
}
