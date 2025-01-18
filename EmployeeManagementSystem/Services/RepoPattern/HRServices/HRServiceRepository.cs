using EmployeeManagementSystem.DataContext;
using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Services.RepoPattern.HRServices;


public class HRServiceRepository : IHRServiceRepository {
	private readonly EmsDataContext dbContext;

	public HRServiceRepository(EmsDataContext dbContext) {
		this.dbContext = dbContext;
	}
	public async Task<List<Employee>> GetAllEmployee() {
		var listEmployee = await dbContext.Employees.Include(s => s.Salary).Include(s => s.Department).Include(s => s.Position).ToListAsync();
		return listEmployee;
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
	public async Task<int> CalculationTotalSalary(string EmployeeCode) {
		var findEmployee = await dbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeCode == EmployeeCode);
		if(findEmployee == null) {
			return 0;
		}
		var salaryEmployee = await dbContext.Salaries.FirstOrDefaultAsync(s => s.SalaryId == findEmployee.SalaryId);

		salaryEmployee.TotalSalary = salaryEmployee.TotalWorkday * salaryEmployee.BaseSalary;
		var result = salaryEmployee.TotalSalary;
		await dbContext.SaveChangesAsync();
		return (int)result;
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

	public async Task<List<string>> GetWorkdayHistory(int salaryId) {
		var workdayCheck = await dbContext.Salaries.FirstOrDefaultAsync(s => s.SalaryId == salaryId);
		return workdayCheck.WorkdayHistory;
	}

	public async Task<List<Salary>> ResetTotalSalaryEmployee() {
		var listDomain = await dbContext.Salaries.ToListAsync();
		foreach(var item in listDomain) {
			item.TotalSalary = 0;
			item.TotalWorkday = 0;
			item.WorkdayHistory.Clear();
		}
		await dbContext.SaveChangesAsync();
		return listDomain;
	}

	public async Task<Salary> SalaryOfEmployee(int salaryId) {
		var salaryEmployee = await dbContext.Salaries.FirstOrDefaultAsync(s => s.SalaryId == salaryId);
		if(salaryEmployee == null) {
			return null;
		}
		return salaryEmployee;
	}

	public async Task<Salary> UpdateSalaryEmployee(Salary newSalary, int SalaryId) {
		var salaryDomain = await dbContext.Salaries.FirstOrDefaultAsync(s => s.SalaryId == SalaryId);
		if(salaryDomain == null) {
			return null;
		}
		salaryDomain.BaseSalary = newSalary.BaseSalary;
		dbContext.Salaries.Update(salaryDomain);
		await dbContext.SaveChangesAsync();
		return salaryDomain;
	}

	public async Task<List<string>> WorkdayCheckIn(string employeeCode, bool CheckIn) {
		if(!CheckIn) {
			return null;
		}
		DateTime dateTime = DateTime.UtcNow;
		var timeString = dateTime.ToString();
		var employCheckIn = await dbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeCode == employeeCode);
		if(employCheckIn == null) {
			return null;
		}
		var salaryEmployee = await dbContext.Salaries.FirstOrDefaultAsync(s => s.SalaryId == employCheckIn.SalaryId);
		if(salaryEmployee == null) {
			return null;
		}
		salaryEmployee.TotalWorkday += 1;
		salaryEmployee.WorkdayHistory.Add(timeString);
		var workHistory = await this.GetWorkdayHistory(salaryEmployee.SalaryId);
		await dbContext.SaveChangesAsync();
		return workHistory;
	}
}
