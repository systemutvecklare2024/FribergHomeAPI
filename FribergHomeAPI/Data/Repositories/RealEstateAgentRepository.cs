using FribergHomeAPI.Models;

namespace FribergHomeAPI.Data.Repositories
{
    public class RealEstateAgentRepository : GenericRepository<RealEstateAgent, ApplicationDbContext>, IRealEstateAgentRepository
    {
        public RealEstateAgentRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
