using AdWorksCore.Web.ControllersView;
using AutoMapper;

namespace AdWorksCore.Web.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ReverseMap();
        }
    }
}
