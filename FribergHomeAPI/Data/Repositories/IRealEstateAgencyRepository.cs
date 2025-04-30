using FribergHomeAPI.Models;

namespace FribergHomeAPI.Data.Repositories
{
    public interface IRealEstateAgencyRepository : IRepository<RealEstateAgency>
    {
        Task<RealEstateAgency> GetWithApplicationsAndAgentsAsync(int id);
    }
}
