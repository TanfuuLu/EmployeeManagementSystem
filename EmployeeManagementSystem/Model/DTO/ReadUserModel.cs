namespace EmployeeManagementSystem.Model.DTO;

public class ReadUserModel {
	public string? EmployeeCode { get; set; }
	public string? FullName { get; set; }
	public string? Gender { get; set; }
	public string? DateOfBirth { get; set; }
	public string? Address { get; set; }
	public DateTime? StartWorkDate { get; set; }
	public string? Status { get; set; }
	public string? LevelWork { get; set; }
	public int DepartmentId { get; set; }
	public int PositionId { get; set; }
	public int SalaryId { get; set; }
}
