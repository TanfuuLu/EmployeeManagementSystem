using EmployeeManagementSystem.Model;

namespace EmployeeManagementSystem.Services.RepoPattern;

public interface IDepartmentRepository {
	Task<List<Department>> GetAllDepartment();
	Task<Department> GetDepartmentById(int id);
	Task<List<Department>> CreateDepartment(Department department);
	Task<Department> UpdateDepartment(int DepartmentId, Department model);
	Task<string> DeleteDepartment(int DepartmentId);

}
