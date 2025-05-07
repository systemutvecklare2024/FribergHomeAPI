using FribergHomeAPI.Models;

namespace FribergHomeAPI.Data.Repositories
{
    public interface IRealEstateAgentRepository : IRepository<RealEstateAgent>
    {
        Task<IEnumerable<RealEstateAgent>> GetAllAgentsAsync();
        Task<RealEstateAgent?> GetByIdWithAgencyAsync(int id);
        Task<RealEstateAgent?> GetApiUserIdAsync(string id);
        Task<RealEstateAgent> GetByIdWithApiUser(int id);
    }
}
