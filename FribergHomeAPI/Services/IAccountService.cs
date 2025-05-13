using FribergHomeAPI.DTOs;
using FribergHomeAPI.Models;
using FribergHomeAPI.Results;
using System.Security.Claims;

namespace FribergHomeAPI.Services
{
    // Author: Christoffer
    public interface IAccountService
    {
        Task<ServiceResult<RealEstateAgent>> RegisterAsync(AccountDTO dto);
        Task<ServiceResult<LoginResult>> LoginAsync(LoginDTO loginDto);
        Task<ServiceResult<RealEstateAgent>> GetMyAgentAsync(ClaimsPrincipal user);
        Task UpdateAsync(UpdateAgentDTO dto, RealEstateAgent existingAgent);
        Task<bool> OwnedBy(ClaimsPrincipal user, int agentId);
    }
}
