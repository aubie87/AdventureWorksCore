using AdWorksCore.Web.Views.Employee;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdWorksCore.Web.Test.Common
{
    /// <summary>
    /// This is a readonly automapper configuration that can be
    /// reused by most tests.
    /// Note - this fixture should not be used when testing the
    /// configuration itself.
    /// </summary>
    public class EmployeeMappingFixture
    {
        public EmployeeMappingFixture()
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EmployeeMappingProfile>();
            });
            mapConfig.AssertConfigurationIsValid();
            Mapper = mapConfig.CreateMapper();
        }

        public IMapper Mapper { get; }
    }
}
