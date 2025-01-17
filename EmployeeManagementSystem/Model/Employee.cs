using Microsoft.AspNetCore.Identity;

namespace EmployeeManagementSystem.Model;

public class Employee : IdentityUser {
	public string? EmployeeCode { get; set; }
	public string? FullName { get; set; }
	public string? Gender { get; set; }
	public string? DateOfBirth { get; set; }
	public string? Address {  get; set; }
	public DateTime? StartWorkDate {  get; set; }
	public string? Status { get; set; }
	public string? LevelWork {  get; set; }

	//Foreign Key
	public int DepartmentId {  get; set; }
	public Department? Department { get; set; }
	public int PositionId {  get; set; }
	public Position? Position { get; set; }	
	public int SalaryId { get; set; }
	public Salary? Salary { get; set; }

}
