using Project.BLL.DTO;
using Project.BLL.Model;
using Project.DAL.Extend;
namespace Project.BLL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Department, DepartmentVM>();
            CreateMap<DepartmentVM, Department>();

            CreateMap<Employee, EmployeeVM>();
            CreateMap<EmployeeVM, Employee>();

            CreateMap<City, CityVM>();
            CreateMap<CityVM, City>();

            CreateMap<Country, CountryVM>();
            CreateMap<CountryVM, Country>();

            CreateMap<District, DistrictVM>();
            CreateMap<DistrictVM, District>();

            CreateMap<SignUpVM, ApplicationUser>();
            CreateMap<ApplicationUser, SignUpVM>();

            CreateMap<ApplicationUser, ApplicationUser>()
           .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeDTO, Employee>();

        }
    }
}
