using System.Globalization;

namespace EmployeeManagementSystem.Model.DTO;

public class RegisterRequestModel {
 
	public string? FullName { get; set; }
	public string? Gender { get; set; }
	public string? DateOfBirth { get; set; }
	public string? Address { get; set; }
	public DateTime? StartWorkDate { get; set; }
	public string? EmployeeEmail {  get; set; }
	public string? Password { get; set; }
	public string? EmployeePhone { get; set; }
	public string? Status { get; set; }
	public string? LevelWork { get; set; }
	public string? Role {  get; set; }
	public int DepartmentId { get; set; }
	public int PositionId {  get; set; }
	
}
