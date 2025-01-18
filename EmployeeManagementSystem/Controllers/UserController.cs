using AutoMapper;
using EmployeeManagementSystem.DataContext;
using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Model.DTO;
using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.Services.RepoPattern;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace EmployeeManagementSystem.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase {
	private readonly UserManager<Employee> userManager;
	private readonly TokenJWTGenerator tokenJWTGenerator;
	private readonly RoleManager<IdentityRole> roleManager;
	private readonly IHRServiceRepository hrServiceRepository;
	private readonly IMapper mapper;

	public UserController(UserManager<Employee> userManager, TokenJWTGenerator tokenJWTGenerator, RoleManager<IdentityRole> roleManager, IHRServiceRepository hrServiceRepository, IMapper mapper) {
		this.userManager = userManager;
		this.tokenJWTGenerator = tokenJWTGenerator;
		this.roleManager = roleManager;
		this.hrServiceRepository = hrServiceRepository;
		this.mapper = mapper;
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody]LoginRequestModel model) {
		var user = await userManager.FindByEmailAsync(model.username);
		if(user == null) {
			return NotFound();
		}else if(await userManager.CheckPasswordAsync(user, model.password)){
			return Unauthorized("Invalid email or password");
		}
		var roles = await userManager.GetRolesAsync(user);
		var token = tokenJWTGenerator.JwtGeneratorToken(user, roles);
		Response.Cookies.Append("AuthToken", token, new CookieOptions {
			HttpOnly = true,
			Secure = true, // Bắt buộc nếu chạy HTTPS
			SameSite = SameSiteMode.None, // Dùng None nếu cross-origin
			Expires = DateTimeOffset.UtcNow.AddHours(2)
		});

		return Ok(token + "\n" + user.FullName);

	}
	[HttpPost("information")]
	public async Task<IActionResult> GetLoginUser(string employeeEmail) {
		var user = await userManager.FindByEmailAsync(employeeEmail);
		var userMap = mapper.Map<ReadUserModel>(user);
		if(user == null) {
			return NotFound();
		}
		return Ok(userMap);
	}
	

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterRequestModel model) {
		
		var employeeCode = EmployeeCodeAuto(model.FullName, model.DateOfBirth);
		var employeeSalary = new Salary {
			SalaryEmployeeCode = employeeCode,
			TotalWorkday = 0,
			TotalSalary = 0,
			BaseSalary = 0,

		};
		var salaryDomain = await hrServiceRepository.CreateSalaryEmployee(employeeSalary);
		var userRegister = new Employee {
			Email = model.EmployeeEmail,
			UserName = model.EmployeeEmail,
			PhoneNumber = model.EmployeePhone,
			PasswordHash = model.Password,
			FullName = model.FullName,
			Gender = model.Gender,
			Status = model.Status,
			LevelWork = model.LevelWork,
			Address = model.Address,
			EmployeeCode = employeeCode,
			StartWorkDate = DateTime.UtcNow,
			DateOfBirth = model.DateOfBirth,
			DepartmentId = model.DepartmentId,
			PositionId = model.PositionId,
			Salary = salaryDomain,
			SalaryId = salaryDomain.SalaryId
		};
		var result = await userManager.CreateAsync(userRegister, model.Password);
		if(!result.Succeeded) {
			return BadRequest(result.Errors);
		}
		if(!string.IsNullOrEmpty(model.Role)) {
			var roleExists = await roleManager.RoleExistsAsync(model.Role);
			if(!roleExists) {
				return BadRequest($"Role '{model.Role}' does not exist.");
			}
			await userManager.AddToRoleAsync(userRegister, model.Role);
		}
		var userResult = await userManager.FindByEmailAsync(userRegister.Email);
		var mapperItem =  mapper.Map<ReadUserModel>(userResult);
		return Ok(userResult);
	}

	[NonAction]
	public string EmployeeCodeAuto(string fullName, string dateOfBirth) {
		if(string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(dateOfBirth)) {
			throw new ArgumentException("Full name and date of birth must not be null or empty.");
		}

		// Extract initials from full name
		var initials = string.Empty;
		var nameParts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		foreach(var part in nameParts) {
			initials += char.ToUpper(part[0]);
		}

		// Parse and format the date of birth
		if(!DateTime.TryParseExact(dateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate)) {
			throw new ArgumentException("Date of birth must be in the format dd/MM/yyyy.");
		}

		var formattedDate = parsedDate.ToString("ddMMyyyy");

		// Combine initials and formatted date
		return initials + formattedDate;
	}
}
