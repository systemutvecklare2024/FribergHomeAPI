using FribergHomeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergHomeAPI.Data.Repositories
{
    public class RealEstateAgencyRepository : GenericRepository<RealEstateAgency, ApplicationDbContext>, IRealEstateAgencyRepository
    {
        private readonly ApplicationDbContext dbContext;

        public RealEstateAgencyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        //Tobias
        public async Task<RealEstateAgency?> GetByIdWithAgentsAsync(int id)
        {
            return await dbContext.Agencies
                .Include(a => a.Agents)
                .Include(a => a.Applications)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
