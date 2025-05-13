using FribergHomeAPI.DTOs;
using FribergHomeAPI.Results;
using System.Security.Claims;

namespace FribergHomeAPI.Services
{
    //Author: Emelie
    public interface IAgencyService
    {
        Task<ServiceResult> HandleApplication(ClaimsPrincipal user, ApplicationDTO applicationDTO);
    }
}
