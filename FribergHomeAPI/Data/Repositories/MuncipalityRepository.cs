using FribergHomeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergHomeAPI.Data.Repositories
{
	public class MuncipalityRepository : IMuncipalityRepository
	{
		private readonly ApplicationDbContext dbContext;

		public MuncipalityRepository(ApplicationDbContext dbContext) 
		{
			this.dbContext = dbContext;
		}
		public async Task<IEnumerable<Muncipality>> GetAllAsync()
		{
			return await dbContext.Muncipalities.ToListAsync();
		}

		public async Task<Muncipality> GetByIdAsync(int id)
		{
			return await dbContext.Muncipalities.FirstOrDefaultAsync(m=>m.Id == id);
		}
	}
}
