using FribergHomeAPI.DTOs;
using FribergHomeAPI.Models;

namespace FribergHomeAPI.Data.Repositories
{
    // Author: Christoffer
    public interface IPropertyRepository : IRepository<Property>
    {
        Task<ICollection<Property>?> FindPropertyInMuncipality(Muncipality muncipality);
        Task<Property?> GetWithAddressAndImages(int id);
        Task<Property?> GetWithAddressAsync(int id);
        Task<IEnumerable<Property>?> GetLatestAsync(int id);

        Task<IEnumerable<Property>?> GetAllMyPropertiesAsync(int agentId);

        //Fredrik
        Task<bool> UpdateAsync(int id, PropertyDTO dto);
    }
}
