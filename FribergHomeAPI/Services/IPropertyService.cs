using FribergHomeAPI.DTOs;
using FribergHomeAPI.Results;

namespace FribergHomeAPI.Services
{
	public interface IPropertyService
	{
		Task<ServiceResult<PropertyDTO>> UpdatePropertyAsync(int id, PropertyDTO dto);
	}
}
