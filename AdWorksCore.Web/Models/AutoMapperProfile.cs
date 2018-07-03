using AdWorksCore.HumanResources.Data.Entities;
using AdWorksCore.Web.ControllersView;
using AdWorksCore.Web.Views.Employee;
using AutoMapper;

namespace AdWorksCore.Web.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ReverseMap();

            CreateMap<Employee, EmployeeViewModel>()
                .ForMember(dest=>dest.Id, opt => opt.MapFrom(src => src.BusinessEntityId))
                .ReverseMap();
        }
    }
}
