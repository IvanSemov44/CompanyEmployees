namespace CompanyEmployees
{
    using AutoMapper;
    using Entities;
    using Shared.DataTransferObjects;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Company 
            CreateMap<Company, CompanyDto>()
               .ForCtorParam("FullAddress",
               opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            CreateMap<CompanyForCreationDto, Company>();

            //Employee
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeForCreationDto, Employee>();
        }
    }
}
