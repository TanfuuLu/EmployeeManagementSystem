using AutoMapper;
using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Model.DTO;
using EmployeeManagementSystem.Services.RepoPattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase {
	private readonly IDepartmentRepository departmentRepository;
	private readonly IMapper mapper;

	public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper) {
		this.departmentRepository = departmentRepository;
		this.mapper = mapper;
	}
	[HttpGet("api/departments/get-list")]
	[Authorize]
	public async Task<IActionResult> GetList() {
		var listDepartment = await departmentRepository.GetAllDepartment();
		return Ok(listDepartment);
	}
	[HttpPost("api/departments/create")]
	public async Task<IActionResult> CreateItem([FromBody] CreateAndUpdateDepartmentModel model) {
		var itemMapper = mapper.Map<Department>(model);
		var itemAdded = await departmentRepository.CreateDepartment(itemMapper);
		return Ok(itemAdded);
	}
	[HttpPut("api/departments/update/{id:int}")]
	public async Task<IActionResult> UpdateItem([FromBody] CreateAndUpdateDepartmentModel model, [FromRoute]int id) {
		var itemMapper = mapper.Map<Department>(model);
		var itemUpdate = await departmentRepository.UpdateDepartment(id,itemMapper);
		return Ok(itemUpdate);
	}
	[HttpDelete("api/departments/delete/{id:int}")]
	public async Task<IActionResult> DeleteItem([FromRoute]int id) {
		var itemResult = await departmentRepository.DeleteDepartment(id);
		return Ok(itemResult);
	}
}
