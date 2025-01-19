using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Model;

public class EventCompany {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int EventId { get; set; }
	public string? EventName { get; set; }
	public string? EventAddress { get; set; }
	public string? EventDescription { get; set; }
	public string? EventAuthors { get; set; }
	public string? EventDate { get; set; }
	
}
