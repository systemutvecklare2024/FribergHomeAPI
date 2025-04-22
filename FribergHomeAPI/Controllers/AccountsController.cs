using AutoMapper;
using FribergHomeAPI.Constants;
using FribergHomeAPI.Data.Repositories;
using FribergHomeAPI.DTOs;
using FribergHomeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FribergHomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UserManager<ApiUser> userManager;
        private readonly IRealEstateAgentRepository agentRepository;

        public AccountsController(IMapper mapper, UserManager<ApiUser> userManager, IRealEstateAgentRepository agentRepository)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.agentRepository = agentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountDTO accountDTO)
        {
            var user = new ApiUser
            {
                FirstName = accountDTO.FirstName,
                LastName = accountDTO.LastName,
                UserName = accountDTO.Email,
                NormalizedUserName = accountDTO.Email.ToUpper(),
                NormalizedEmail = accountDTO.Email.ToUpper(),
                Email = accountDTO.Email,
            };

            var userResult = await userManager.CreateAsync(user, accountDTO.Password);

            if (!userResult.Succeeded)
            {
                foreach(var error in userResult.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            await userManager.AddToRoleAsync(user, ApiRoles.Agent);

            var agent = new RealEstateAgent
            {
                FirstName = accountDTO.FirstName,
                LastName = accountDTO.LastName,
                Email = accountDTO.Email,
                PhoneNumber = accountDTO.PhoneNumber,
                ImageUrl = accountDTO.ImageUrl,
                ApiUserId = user.Id,
                AgencyId = 1,
            };

            await agentRepository.AddAsync(agent);

            return Created();
        }
    }
}
