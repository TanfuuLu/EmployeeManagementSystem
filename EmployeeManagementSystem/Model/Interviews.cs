using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Model;

public class Interviews {
	[Key]
	public int InterviewId { get; set; }
	public string? IntervieweeName { get; set; }
	public string? TimeInterview { get; set; }
	public string? Interviewer { get; set; }
	public string? IntervieweeEmail { get; set; }
	public string? IntervieweePhone { get; set; }
	public string? IntervieweeCVLinks { get; set; }
	public bool Status { get; set; }
}
