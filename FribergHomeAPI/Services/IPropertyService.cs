using FribergHomeAPI.DTOs;

namespace FribergHomeAPI.Services
{
	public interface IPropertyService
	{
		Task UpdatePropertyAsync(int id, PropertyDTO dto);
	}
}
