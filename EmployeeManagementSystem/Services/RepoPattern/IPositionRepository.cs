using EmployeeManagementSystem.DataContext;
using EmployeeManagementSystem.Model;

namespace EmployeeManagementSystem.Services.RepoPattern;

public interface IPositionRepository {
	Task<List<Position>> GetList();
	Task<List<Position>> CreateItem(Position position);
	Task<string> DeleteItem(int id);
	Task<List<Position>> UpdateItem(int id, Position position);	
	Task<Position> GetPositionById(int id);
	
}
