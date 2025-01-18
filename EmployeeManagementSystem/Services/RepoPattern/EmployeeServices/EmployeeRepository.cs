using EmployeeManagementSystem.DataContext;
using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Services.RepoPattern.EmployeeServices;

public class EmployeeRepository : IEmployeeRepository {
	private readonly EmsDataContext dbContext;

	public EmployeeRepository(EmsDataContext dbContext) {
		this.dbContext = dbContext;
	}

	public async Task<Employee> DeleteEmployee(string employeeCode) {
		var findEmployee = await dbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeCode == employeeCode);
		if(findEmployee == null) {
			return null;
		}
		dbContext.Employees.Remove(findEmployee);
		await dbContext.SaveChangesAsync();
		return findEmployee;
	}

	

	public async Task<List<Employee>> GetEmployeeByDepartment(int department) {
		var listEmployee = await dbContext.Employees.Where(s => s.DepartmentId == department)
			.Include(e => e.Department)
			.Include(e => e.Position)
			.ToListAsync();
		return listEmployee;
	}

	public async Task<List<Employee>> GetEmployeeByPosition(int position) {
		var listEmployee = await dbContext.Employees.Where(s => s.PositionId == position)
			.Include(e => e.Department)
			.Include(e => e.Position)
			.ToListAsync();
		return listEmployee;
	}

	public async Task<Employee> UpdateEmployee(Employee employeeUpdate, string employeeCode) {
		var findEmployee = await dbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeCode == employeeCode);
		if(findEmployee == null) {
			return null;
		}
		findEmployee.LevelWork = employeeUpdate.LevelWork;
		findEmployee.PositionId = employeeUpdate.PositionId;
		findEmployee.DepartmentId = employeeUpdate.DepartmentId;
		await dbContext.SaveChangesAsync();
		return findEmployee;
	}
}
