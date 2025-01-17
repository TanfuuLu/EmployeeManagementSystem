using EmployeeManagementSystem.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.DataContext;

public class EmsDataContext : IdentityDbContext<Employee> {
	public EmsDataContext(DbContextOptions options) : base(options) {
	}
	public DbSet<Employee> Employees { get; set; }
	public DbSet<Department> Departments { get; set; }
	public DbSet<Position> Positions { get; set; }
	public DbSet<Salary> Salaries { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);

		// Seed dữ liệu cho các vai trò
		modelBuilder.Entity<IdentityRole>().HasData(
		    new IdentityRole {
			    Id = "1", // ID cần là duy nhất
			    Name = "Admin",
			    NormalizedName = "ADMIN"
		    },
		    new IdentityRole {
			    Id = "2",
			    Name = "Manager",
			    NormalizedName = "MANAGER"
		    },
		    new IdentityRole {
			    Id = "3",
			    Name = "Employee",
			    NormalizedName = "EMPLOYEE"
		    }
		);
	}
}
