using FribergHomeAPI.DTOs;
using FribergHomeAPI.Results;

namespace FribergHomeAPI.Services
{
    // Author: Christoffer
    public interface IAccountService
    {
        Task<RegistrationResult> RegisterAsync(AccountDTO accountDto);
        Task<LoginResult> LoginAsync(LoginDTO loginDto);
    }
}
