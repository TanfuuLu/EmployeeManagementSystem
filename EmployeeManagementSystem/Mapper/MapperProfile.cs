using AutoMapper;
using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Model.DTO;
using EmployeeManagementSystem.Model.DTO.EmployeeServiceDTO;
using EmployeeManagementSystem.Model.DTO.SalaryServiceDTO;

namespace EmployeeManagementSystem.Mapper;

public class MapperProfile : Profile {
	public MapperProfile() {
		CreateMap<Department,CreateAndUpdateDepartmentModel>().ReverseMap();
		CreateMap<Position,CreateAndUpdatePositionModel>().ReverseMap();
		CreateMap<Employee,ReadUserModel>().ReverseMap();
		CreateMap<Salary,UpdateSalaryDTO>().ReverseMap();
		CreateMap<Employee, UpdateEmployeeDTO>().ReverseMap();
	}
}
