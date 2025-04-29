using AutoMapper;
using FribergHomeAPI.Constants;
using FribergHomeAPI.Data.Repositories;
using FribergHomeAPI.DTOs;
using FribergHomeAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FribergHomeAPI.Controllers
{
    //Author: Grupp Charlie
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UserManager<ApiUser> userManager;
        private readonly IRealEstateAgentRepository agentRepository;
        private readonly IConfiguration configuration;

        public AccountsController(
            IMapper mapper, 
            UserManager<ApiUser> userManager, 
            IRealEstateAgentRepository agentRepository,
            IConfiguration configuration)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.agentRepository = agentRepository;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(AccountDTO accountDTO)
        {
            //Begin Transaction
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
                AgencyId = 1, //ToDo: Handle RealEstateAgency
            };

            await agentRepository.AddAsync(agent);
            //End Transaction

            return Created(uri: $"/api/agents/{agent.Id}", agent);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            var validPassword = await userManager.CheckPasswordAsync(user, loginDto.Password);

            if(user == null || !validPassword)
            {
                 return Unauthorized();
            }

            string token = await GenerateToken(user);
            var agent = await agentRepository.FirstOrDefaultAsync(a => a.ApiUserId == user.Id);
            if (agent == null) 
            {
                return Unauthorized();
            }
            var response = new AuthResponse
            {
                Email = loginDto.Email,
                UserId = user.Id,
                Token = token,
                AgentId = agent.Id
            };

            return Ok(response);
        }

        private async Task<string> GenerateToken(ApiUser apiUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[Settings.Key]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var roles =  await userManager.GetRolesAsync(apiUser);
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();
            var userClaims = await userManager.GetClaimsAsync(apiUser);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, apiUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, apiUser.Email),
                new Claim(CustomClaimTypes.Uid, apiUser.Id)
            }.Union(roleClaims).Union(userClaims);

            var token = new JwtSecurityToken(
                issuer: configuration[Settings.Issuer],
                audience: configuration[Settings.Audience],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration[Settings.Duration])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpGet("MyProfile")]
        public async Task<IActionResult> GetProfile()
        {
            //Get logged in ApiUser
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized();
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(userId);
        }
    }
}
