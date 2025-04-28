using FribergHomeAPI.Models;

namespace FribergHomeAPI.Data.Repositories
{
    // Author: Christoffer
    public interface IPropertyRepository : IRepository<Property>
    {
        Task<ICollection<Property>?> FindPropertyInMuncipality(Muncipality muncipality);
        Task<Property?> GetWithAddressAndImages(int id);
        Task<Property?> GetWithAddressAsync(int id);
    }
}
