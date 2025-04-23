using FribergHomeAPI.Models;


namespace FribergHomeAPI.Data.Repositories
{
    public class RealEstateAgencyRepository : GenericRepository<RealEstateAgency, ApplicationDbContext>, IRealEstateAgencyRepository
    {
        public RealEstateAgencyRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
