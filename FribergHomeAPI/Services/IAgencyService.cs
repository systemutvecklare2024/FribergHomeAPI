using FribergHomeAPI.DTOs;
using FribergHomeAPI.Results;
using Microsoft.AspNetCore.Mvc;

namespace FribergHomeAPI.Services
{
    //Author: Emelie
    public interface IAgencyService
    {
        Task<ServiceResult<bool>> ApproveApplication(ApplicationDTO applicationDTO);
        Task<ServiceResult<bool>> DenyApplication(ApplicationDTO applicationDTO);
        Task<ServiceResult<bool>> HandelApplication(ApplicationDTO applicationDTO);
    }
}
