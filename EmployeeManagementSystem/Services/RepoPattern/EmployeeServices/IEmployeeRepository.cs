﻿using EmployeeManagementSystem.Model;

namespace EmployeeManagementSystem.Services.RepoPattern.EmployeeServices;

public interface IEmployeeRepository {
	Task<Employee> UpdateEmployee(Employee employee, string employeeCode);
	Task<List<Employee>> GetEmployeeByDepartment(int department);
	Task<List<Employee>> GetEmployeeByPosition(int position);
}
