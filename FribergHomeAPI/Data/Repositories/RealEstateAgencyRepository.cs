using FribergHomeAPI.DTOs;
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

        public async Task<bool> AddApplication(int agentId, int agencyId)
        {
            try
            {
                var agency = await GetByIdWithAgentsAsync(agencyId);
                if (agency == null)
                {
                    return false;
                }
                var application = new Application
                {
                    AgencyId = agencyId,
                    AgentId = agentId
                };

                agency.Applications.Add(application);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}