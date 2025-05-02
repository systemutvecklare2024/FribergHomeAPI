using FribergHomeAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FribergHomeAPI.Services
{
    //Author: Emelie
    public interface IAgencyService
    {
        Task<IActionResult> ApproveApplication(ApplicationDTO applicationDTO);
        Task<IActionResult> DenyApplication(ApplicationDTO applicationDTO);
        Task<IActionResult> HandelApplication(ApplicationDTO applicationDTO);
    }
}
