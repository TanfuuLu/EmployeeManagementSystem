namespace EmployeeManagementSystem.Model.DTO;

public class CreateLeaveWorkDTO {
	public string? EmployeeCode { get; set; }
	public string? StartDay { get; set; }
	public string? EndDay { get; set; }
	public string? LeaveTypeId { get; set; }
	public string? LeaveReason { get; set; }
	public string? DateLeaveRequest { get; set; }
}
