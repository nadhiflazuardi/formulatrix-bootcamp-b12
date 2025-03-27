using AutoMapper;
using EmployeeAdminPortal.DTOs;
using EmployeeAdminPortal.Models;

namespace EmployeeAdminPortal.MappingProfiles;

public class EmployeeMappingProfile : Profile
{
  public EmployeeMappingProfile()
  {
    CreateMap<Employee, EmployeeDTO>().ReverseMap();

    CreateMap<EmployeeCreateDTO, Employee>();

    CreateMap<EmployeeUpdateDTO, Employee>();
  }
}