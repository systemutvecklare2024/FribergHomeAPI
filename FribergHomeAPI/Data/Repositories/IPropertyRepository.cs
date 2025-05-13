using FribergHomeAPI.DTOs;
using FribergHomeAPI.Models;

namespace FribergHomeAPI.Data.Repositories
{
    // Author: Christoffer
    public interface IPropertyRepository : IRepository<Property>
    {
        Task<ICollection<Property>?> FindPropertyInMuncipality(Muncipality muncipality);
		Task<ICollection<Property>?> GetByMuncipalityId(int muncipalityId);
		Task<Property?> GetWithAddressImagesAndMuncipality(int id);
        Task<Property?> GetWithAddressAsync(int id);
        Task<IEnumerable<Property>?> GetLatestAsync(int id);

        Task<IEnumerable<Property>?> GetAllPropertiesByAgentIdAsync(int agentId);

        //Fredrik
        Task<bool> UpdateAsync(int id, PropertyDTO dto);
        Task UpdateAsync(Property property, List<PropertyImage> imagesToDelete);
    }
}
