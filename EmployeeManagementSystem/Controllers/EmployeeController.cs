using AutoMapper;
using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Model.DTO.EmployeeServiceDTO;
using EmployeeManagementSystem.Services.RepoPattern.EmployeeServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase {
	private readonly IEmployeeRepository employeeRepository;
	private readonly IMapper mapper;

	public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper) {
		this.employeeRepository = employeeRepository;
		this.mapper = mapper;
	}
	
	[HttpGet("get-employee-by-department/{department}")]
	public async Task<IActionResult> GetEmployeeByDepartment(int department) {
		var listEmployee = await employeeRepository.GetEmployeeByDepartment(department);
		return Ok(listEmployee);
	}
	[HttpGet("get-employee-by-position/{position}")]
	public async Task<IActionResult> GetEmployeeByPosition(int position) {
		var listEmployee = await employeeRepository.GetEmployeeByPosition(position);
		return Ok(listEmployee);
	}
	[HttpPut("update-employee/{employeeCode}")]
	public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeDTO employee, string employeeCode) {
		var empDomain = mapper.Map<Employee>(employee);
		var employeeUpdate = await employeeRepository.UpdateEmployee(empDomain, employeeCode);
		if(employeeUpdate == null) {
			return NotFound();
		}
		return Ok(employeeUpdate);
	}
}
