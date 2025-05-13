using FribergHomeAPI.DTOs;
using FribergHomeAPI.Results;
using System.Security.Claims;

namespace FribergHomeAPI.Services
{
	public interface IPropertyService
	{
        Task<ServiceResult<PropertyDTO>> UpdatePropertyAsync(ClaimsPrincipal user, int id, PropertyDTO dto);
        Task<ServiceResult> DeleteAsync(ClaimsPrincipal user, int id);

    }
}
