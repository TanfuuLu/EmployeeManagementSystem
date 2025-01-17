using AutoMapper;
using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Model.DTO;
using EmployeeManagementSystem.Services.RepoPattern;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PositionController : ControllerBase {
	private readonly IPositionRepository positionRepository;
	private readonly IMapper mapper;

	public PositionController(IPositionRepository positionRepository, IMapper mapper) {
		this.positionRepository = positionRepository;
		this.mapper = mapper;
	}
	[HttpGet("api/positions/get-list")]
	public async Task<IActionResult> GetList() {
		var list = await positionRepository.GetList();
		return Ok(list);
	}
	[HttpPost("api/positions/create")]
	public async Task<IActionResult> CreateItem([FromBody] CreateAndUpdatePositionModel model) {
		var itemMap = mapper.Map<Position>(model);
		var itemResult = await positionRepository.CreateItem(itemMap);
		return Ok(itemResult);
	}
	[HttpPut("api/positions/update/{id:int}")]
	public async Task<IActionResult> UpdateItem([FromBody] CreateAndUpdatePositionModel model, [FromRoute]int id) {
		var itemMap = mapper.Map<Position>(model);
		var itemResult = await positionRepository.UpdateItem(id, itemMap);	
		return Ok(itemResult);
	}
	[HttpDelete("api/positions/delete/{id:int}")]
	public async Task<IActionResult> DeleteItem([FromRoute]int id) {
		var result = await positionRepository.DeleteItem(id);
		return Ok(result);
	}
}
