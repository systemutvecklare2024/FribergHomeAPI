using FribergHomeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergHomeAPI.Data.Repositories
{
    public class RealEstateAgencyRepository : GenericRepository<RealEstateAgency, ApplicationDbContext>, IRealEstateAgencyRepository
    {

        public RealEstateAgencyRepository(ApplicationDbContext dbContext) : base(dbContext){}

        public async Task<RealEstateAgency> GetWithApplicationsAndAgentsAsync(int id)
        {
            return await DbContext.Agencies
                .Include(a => a.Applications)
                .Include(a => a.Agents)
                .FirstOrDefaultAsync(a =>  a.Id == id);
        }
    }
}
