using FribergHomeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergHomeAPI.Data.Repositories
{
    public class RealEstateAgentRepository : GenericRepository<RealEstateAgent, ApplicationDbContext>, IRealEstateAgentRepository
    {
        private readonly ApplicationDbContext dbContext;

        public RealEstateAgentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<RealEstateAgent>> GetAllAgentsAsync()
        {
            return await dbContext.Agents
                .Include(a => a.Agency)
                .Include(p => p.Properties)
                .ToListAsync();
        }
    }
}
