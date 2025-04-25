using FribergHomeAPI.Models;

namespace FribergHomeAPI.Data.Repositories
{
    public interface IRealEstateAgentRepository : IRepository<RealEstateAgent>
    {
        Task<IEnumerable<RealEstateAgent>> GetAllAgentsAsync();
        Task<RealEstateAgent?> GetByIdWithAgencyAsync(int id);
    }
}
