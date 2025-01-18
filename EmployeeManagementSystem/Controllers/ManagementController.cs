using AutoMapper;
using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Model.DTO.SalaryServiceDTO;
using EmployeeManagementSystem.Services.RepoPattern;
using EmployeeManagementSystem.Services.RepoPattern.EmployeeServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ManagementController : ControllerBase {
	private readonly IHRServiceRepository hrRepository;
	private readonly IMapper mapper;

	public ManagementController(IHRServiceRepository hrRepository, IMapper mapper) {
		this.hrRepository = hrRepository;
		this.mapper = mapper;
	}
	[HttpPost("salary/calculation-salary")]
	public async Task<IActionResult> CalcuTotalSalary(string EmployeeCode) {
		var result = await hrRepository.CalculationTotalSalary(EmployeeCode);
		return Ok("Total Salary: "+ result);
	}
	[HttpPost("salary/daily-checkIn")]
	public async Task<IActionResult> CheckIn(string EmployeeCode, bool CheckIn) {
		var result = await hrRepository.WorkdayCheckIn(EmployeeCode, CheckIn);
		if(result != null) {
			return Ok(result);
		}	
		return NotFound("Check In F1ailed");
	}
	[HttpPost("salary/check-salary")]
	public async Task<IActionResult> CheckSalary(int SalaryId) {
		var result = await hrRepository.SalaryOfEmployee(SalaryId);
		if(result != null) {
			return Ok(result);
		}
		return NotFound("Salary not found");
	}
	[HttpPut("salary/update-salary")]	
	public async Task<IActionResult> UpdateSalary(UpdateSalaryDTO newSalary, int SalaryId) {
		var salaryDTO = mapper.Map<Salary>(newSalary);
		var result = await hrRepository.UpdateSalaryEmployee(salaryDTO, SalaryId);
		if(result != null) {
			return Ok(result);
		}
		return NotFound("Update failed");
	}
	[HttpGet("salary/reset-salary")]
	public async Task<IActionResult> ResetSalary() {
		var result = await hrRepository.ResetTotalSalaryEmployee();
		if(result != null) {
			return Ok(result);
		}
		return NotFound("Reset failed");
	}
	[HttpDelete("employee/delete-employee")]
	public async Task<IActionResult> DeleteEmployee(string EmployeeCode) {
		var result = await hrRepository.DeleteEmployee(EmployeeCode);
		if(result != null) {
			return Ok(result);
		}
		return NotFound("Delete failed");
	}
	[HttpGet("employee/get-all-employee")]
	public async Task<IActionResult> GetListEmployee() {
		var listEmployee = await hrRepository.GetAllEmployee();
		return Ok(listEmployee);
	}
}
