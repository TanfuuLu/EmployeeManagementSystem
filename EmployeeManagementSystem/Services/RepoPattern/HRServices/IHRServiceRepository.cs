﻿using EmployeeManagementSystem.Model;

namespace EmployeeManagementSystem.Services.RepoPattern;

public interface IHRServiceRepository {
	Task<List<string>> WorkdayCheckIn(string employeeCode, bool checkIn);
	Task<int> CalculationTotalSalary(string EmployeeCode);
	Task<Salary> SalaryOfEmployee(int salaryId);
	Task<Salary> CreateSalaryEmployee(Salary employee);
	Task<List<string>> GetWorkdayHistory(int salaryId);
	Task<Salary> UpdateSalaryEmployee(Salary newSalary, int SalaryId);
	Task<List<Salary>> ResetTotalSalaryEmployee();
	Task<Employee> DeleteEmployee(string employeeCode); 
	Task<List<Employee>> GetAllEmployee();
	Task<Employee> GetEmployeeByCode(string code);
	Task<List<LeaveWork>> GetLeaveWorksByStatus(string status);
	Task<List<LeaveWork>> GetLeaveWorksByEmployeeCode(string employeeCode);

}

