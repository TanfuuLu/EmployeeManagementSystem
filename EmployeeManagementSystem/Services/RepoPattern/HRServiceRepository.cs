using EmployeeManagementSystem.DataContext;
using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Services.RepoPattern;


public class HRServiceRepository : IHRServiceRepository {
	private readonly EmsDataContext dbContext;

	public HRServiceRepository(EmsDataContext dbContext) {
		this.dbContext = dbContext;
	}

	public async Task<int> CalculationTotalSalary(string EmployeeCode) {
		var findEmployee = await dbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeCode == EmployeeCode);
		if(findEmployee == null) {
			return 0;
		}
		findEmployee.Salary.TotalSalary = (int)findEmployee.Salary.TotalWorkday * findEmployee.Salary.BaseSalary;
		var result = findEmployee.Salary.TotalSalary;
		return result;

	}

	public async Task<Salary> CreateSalaryEmployee(Salary employee) {
		if(employee == null) {
			return null;
		}
		dbContext.Salaries.Add(employee);
		await dbContext.SaveChangesAsync();
		var findItem = await dbContext.Salaries.FirstOrDefaultAsync(s => s.SalaryEmployeeCode == employee.SalaryEmployeeCode);
		return findItem;
	}

	public async Task<Salary> SalaryOfEmployee(string EmployeeCode) {
		var findEmployee = await dbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeCode == EmployeeCode);
		if(findEmployee == null) {
			return null;
		}
		return findEmployee.Salary;
	}

	public async Task<bool> WorkdayCheckIn(string employeeCode, bool CheckIn) {
		if(!CheckIn) {
			return false;
		}
		DateTime dateTime = DateTime.UtcNow;
		var timeString = dateTime.ToString();
		var employCheckIn = await dbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeCode == employeeCode);
		if(employCheckIn == null) {
			return false;
		}
		employCheckIn.Salary.TotalWorkday += 1;
		employCheckIn.Salary.WorkdayHistory.Add(timeString);
		return true;
	}
}
