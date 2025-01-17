using EmployeeManagementSystem.DataContext;
using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Services.RepoPattern;

public class DepartmentRepository : IDepartmentRepository {
	private readonly EmsDataContext dbContext;

	public DepartmentRepository(EmsDataContext dbContext) {
		this.dbContext = dbContext;
	}

	public async Task<List<Department>> CreateDepartment(Department department) {
		if(department != null) {
			dbContext.Departments.Add(department);
			await dbContext.SaveChangesAsync();
		}
		var list = await dbContext.Departments.ToListAsync();
		return list;
	}

	public async Task<string> DeleteDepartment(int DepartmentId) {
		var findItem = await dbContext.Departments.FirstOrDefaultAsync(d => d.DepartmentId == DepartmentId);
		if(findItem != null) {
			dbContext.Departments.Remove(findItem);
			await dbContext.SaveChangesAsync();	
		}
		return "Deleted Success";
	}

	public async Task<List<Department>> GetAllDepartment() {
		var listResult = await dbContext.Departments.ToListAsync();
		return listResult;
	}

	public async Task<Department> GetDepartmentById(int id) {
		var findItem = await dbContext.Departments.FirstOrDefaultAsync(d => d.DepartmentId == id);
		if(findItem != null) {
			return findItem;
		} else {
			return null;
		}
	}

	public async Task<Department> UpdateDepartment(int DepartmentId, Department model) {
		var findItem = await dbContext.Departments.FirstOrDefaultAsync(d => d.DepartmentId == DepartmentId);
		if(findItem != null && model != null) {
			findItem.DepartmentName = model.DepartmentName;
		}
		dbContext.Departments.Update(findItem);
		await dbContext.SaveChangesAsync();
		return findItem;
	}
}
