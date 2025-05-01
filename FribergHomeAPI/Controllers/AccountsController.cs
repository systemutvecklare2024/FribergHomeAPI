using AutoMapper;
using FribergHomeAPI.DTOs;
using FribergHomeAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FribergHomeAPI.Controllers
{
    //Author: Grupp Charlie
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IAccountService accountService;

        public AccountsController(IMapper mapper, IAccountService accountService)
        {
            this.mapper = mapper;
            this.accountService = accountService;
        }

        // Author: Charlie, Rewrite: Christoffer
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(AccountDTO accountDTO)
        {
            var result = await accountService.RegisterAsync(accountDTO);
            if (!result.Success)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Created(uri: $"/api/RealEstateAgents/{result.Agent!.Id}", result.Agent);
        }

        [HttpPost]
        [Route("login")]
        // Author: Charlie, Co-Auth: Tobias, Rewrite: Christoffer
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var result = await accountService.LoginAsync(loginDto);

            if (!result.Success)
            {
                foreach(var error in result.Errors!)
                {
                    ModelState.AddModelError("", error);
                }

                return BadRequest(ModelState);
            }
            
            var response = new AuthResponse
            {
                Email = result.Email!,
                UserId = result.UserId!,
                Token = result.Token!,
                AgentId = result.AgentId!.Value
            };

            return Ok(response);
        }
    }
}
