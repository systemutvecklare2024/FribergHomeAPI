using FribergHomeAPI.Models;

namespace FribergHomeAPI.Data.Repositories
{
	public interface IMuncipalityRepository
	{
		Task<IEnumerable<Muncipality>> GetAllAsync();
		Task<Muncipality> GetByIdAsync(int id);
	}
}
