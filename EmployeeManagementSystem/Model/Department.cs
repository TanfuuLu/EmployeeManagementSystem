using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Model;
//Phong ban
public class Department {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int DepartmentId { get; set; }
	public string? DepartmentName { get; set; }
	//Foreign Key
	public ICollection<Position> Positions { get; set; } = new List<Position>();
	public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
