using AutoMapper;
using System.Diagnostics;

namespace AdWorksCore.Web.Views.Employee
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            Debug.WriteLine("EmployeeMappingProfile ctor");

            CreateMap<HumanResources.Data.Entities.Employee, EmployeeViewModel>()
                // Employee
                .ForMember(vm => vm.Id, emp => emp.MapFrom(e => e.BusinessEntityId))
                .ForMember(vm => vm.EmployeeModifiedDate, emp => emp.MapFrom(e => e.ModifiedDate))
                // Person
                .ForMember(vm => vm.Id, emp => emp.MapFrom(e => e.BusinessEntity.BusinessEntityId))
                //.ForMember(vm => vm.PersonType, emp => emp.MapFrom(e => e.BusinessEntity.PersonType))
                .ForMember(vm => vm.NameStyle, emp => emp.MapFrom(e => e.BusinessEntity.NameStyle))
                .ForMember(vm => vm.Title, emp => emp.MapFrom(e => e.BusinessEntity.Title))
                .ForMember(vm => vm.FirstName, emp => emp.MapFrom(e => e.BusinessEntity.FirstName))
                .ForMember(vm => vm.MiddleName, emp => emp.MapFrom(e => e.BusinessEntity.MiddleName))
                .ForMember(vm => vm.LastName, emp => emp.MapFrom(e => e.BusinessEntity.LastName))
                .ForMember(vm => vm.Suffix, emp => emp.MapFrom(e => e.BusinessEntity.Suffix))
                .ForMember(vm => vm.EmailPromotion, emp => emp.MapFrom(e => e.BusinessEntity.EmailPromotion))
                .ForMember(vm => vm.PersonLastModified, emp => emp.MapFrom(e => e.BusinessEntity.ModifiedDate))
                .ReverseMap();
                //.ForMember(emp => emp.BusinessEntity.PersonType, opt => opt.UseValue<string>("EM"));

            // seems unneccessary - should just work without explicit mapping.
            //CreateMap<HumanResources.Data.Entities.Person, HumanResources.Data.Entities.Person>();
            //CreateMap<HumanResources.Data.Entities.Employee, HumanResources.Data.Entities.Employee>();
        }
    }
}
