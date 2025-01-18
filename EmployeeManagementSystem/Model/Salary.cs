using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Model;

public class Salary {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int SalaryId { get; set; }
	public int? BaseSalary { get; set; }
	public int? TotalSalary { get; set; }
	public List<string>? WorkdayHistory { get; set; } = new List<string>();
	public int? TotalWorkday { get; set; }
	public string? SalaryEmployeeCode { get; set; }

	public Employee? Employee { get; set; }

}
