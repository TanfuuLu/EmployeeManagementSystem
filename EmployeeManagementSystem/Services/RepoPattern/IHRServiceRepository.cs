using EmployeeManagementSystem.Model;

namespace EmployeeManagementSystem.Services.RepoPattern;

public interface IHRServiceRepository {
	Task<bool> WorkdayCheckIn( string employeeCode, bool checkIn);
	Task<int> CalculationTotalSalary( string EmployeeCode);
	Task<Salary> SalaryOfEmployee(string EmployeeCode);
	Task<Salary> CreateSalaryEmployee(Salary employee);

}
