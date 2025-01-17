using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Model;

public class Position {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int PositionId {  get; set; }
	public string? PositionName { get; set; }
	public int DepartmentId { get; set; }
	public Department? Department { get; set; }
}