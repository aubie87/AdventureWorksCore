using System;
using System.Collections.Generic;

namespace AdWorksCore.HumanResources.Data.Entities
{
    public partial class CountryRegion
    {
        public CountryRegion()
        {
            StateProvince = new HashSet<StateProvince>();
        }

        public string CountryRegionCode { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<StateProvince> StateProvince { get; set; }
    }
}
