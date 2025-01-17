using EmployeeManagementSystem.DataContext;
using EmployeeManagementSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Services.RepoPattern;

public class PositionRepository : IPositionRepository {
	private readonly EmsDataContext dbContext;

	public PositionRepository(EmsDataContext dbContext) {
		this.dbContext = dbContext;
	}

	public async Task<List<Position>> CreateItem(Position position) {
		if(position != null) {
			await dbContext.Positions.AddAsync(position);
			await dbContext.SaveChangesAsync();
		}
		var list = await dbContext.Positions.ToListAsync();
		return list;
	}

	public async Task<string> DeleteItem(int id) {
		var findItem = await dbContext.Positions.FirstOrDefaultAsync(p => p.PositionId == id);
		if(findItem != null) {
			dbContext?.Positions.Remove(findItem);
			await dbContext.SaveChangesAsync();
		}
		return "Deleted Success";
	}

	public async Task<List<Position>> GetList() {
		var list = await dbContext.Positions.ToListAsync();
		return list;
	}

	public async Task<Position> GetPositionById(int id) {
		var findItem = await dbContext.Positions.FirstOrDefaultAsync(p => p.PositionId == id);
		if(findItem != null) {
			return findItem;
		}
		return null;
	}

	public async Task<List<Position>> UpdateItem(int id, Position position) {
		var findItem = await dbContext.Positions.FirstOrDefaultAsync(p => p.PositionId == id);
		if(findItem != null) {
			findItem.PositionName = position.PositionName;
		}
		dbContext?.Positions.Update(findItem);
		await dbContext.SaveChangesAsync();
		var list = await dbContext.Positions.ToListAsync();
		return list;
	}
}
