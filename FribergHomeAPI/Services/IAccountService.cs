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
        Task<ServiceResult<int>> GetMyAgentIdAsync(ClaimsPrincipal user);
    }
}
