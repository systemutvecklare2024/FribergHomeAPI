using FribergHomeAPI.Models;

namespace FribergHomeAPI.Data.Repositories
{
    // Author: Christoffer
    public interface IPropertyRepository : IRepository<Property>
    {
        Task<ICollection<Property>?> FindPropertyInMuncipality(Muncipality muncipality);
        Task<Property?> GetWithAddressAsync(int id);

        Task<IEnumerable<Property>?> GetLatestAsync(int id);
    }
}
