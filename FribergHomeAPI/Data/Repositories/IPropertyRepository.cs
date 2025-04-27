using FribergHomeAPI.DTOs;
using FribergHomeAPI.Models;

namespace FribergHomeAPI.Data.Repositories
{
    // Author: Christoffer
    public interface IPropertyRepository : IRepository<Property>
    {
        Task<ICollection<Property>?> FindPropertyInMuncipality(Muncipality muncipality);
        Task<Property?> GetWithAddressAsync(int id);
        //Fredrik
        Task<bool> UpdateAsync(int id, PropertyDTO dto);
    }
}
