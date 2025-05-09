using FribergHomeAPI.DTOs;
using FribergHomeAPI.Models;

namespace FribergHomeAPI.Data.Repositories
{
    public interface IRealEstateAgentRepository : IRepository<RealEstateAgent>
    {
        Task<IEnumerable<RealEstateAgent>> GetAllAgentsAsync();
        Task<RealEstateAgent?> GetByIdWithAgencyAsync(int id);
        Task<RealEstateAgent?> GetApiUserIdAsync(string id);
        Task<RealEstateAgent> GetByIdWithApiUser(int id);
        Task<RealEstateAgent> GetbyEmailAsync(string email);
    }
}
