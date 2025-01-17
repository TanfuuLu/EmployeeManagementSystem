using AutoMapper;
using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Model.DTO;

namespace EmployeeManagementSystem.Mapper;

public class MapperProfile : Profile {
	public MapperProfile() {
		CreateMap<Department,CreateAndUpdateDepartmentModel>().ReverseMap();
		CreateMap<Position,CreateAndUpdatePositionModel>().ReverseMap();
		CreateMap<Employee,ReadUserModel>().ReverseMap();
	}
}
