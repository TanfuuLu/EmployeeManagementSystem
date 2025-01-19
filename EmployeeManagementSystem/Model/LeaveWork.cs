namespace EmployeeManagementSystem.Model;

public class LeaveWork {
	public int LeaveId { get; set; }
	public string? EmployeeCode { get; set; }	
	public Employee? Employee { get; set; }
	public string? StartDay { get; set; }
	public string? EndDay { get; set; }	
	public string? LeaveTypeId { get; set; }
	public string? LeaveReason { get; set; }
	public string? DateLeaveRequest { get; set; }
	public string? Status { get; set; }
	public string? Approver { get; set; }
}


