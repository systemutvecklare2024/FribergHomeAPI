using AutoMapper;
using FribergHomeAPI.Constants;
using FribergHomeAPI.Data;
using FribergHomeAPI.Data.Repositories;
using FribergHomeAPI.DTOs;
using FribergHomeAPI.Models;
using FribergHomeAPI.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FribergHomeAPI.Services
{
    // Author: Christoffer
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApiUser> userManager;
        private readonly IRealEstateAgentRepository agentRepository;
        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration configuration;
        private readonly IAgencyService agencyService;
        private readonly IMapper mapper;

        public AccountService(UserManager<ApiUser> userManager,
            IRealEstateAgentRepository agentRepository,
            ApplicationDbContext applicationDbContext,
            IConfiguration configuration,
            IAgencyService agencyService,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.agentRepository = agentRepository;
            this.dbContext = applicationDbContext;
            this.configuration = configuration;
            this.agencyService = agencyService;
            this.mapper = mapper;
        }

        public async Task<ServiceResult<LoginResult>> LoginAsync(LoginDTO loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return ServiceResult<LoginResult>.Failure("Ogiltig email eller lösenord");
            }
            var validPassword = await userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!validPassword)
            {
                return ServiceResult<LoginResult>.Failure("Ogiltig email eller lösenord");
            }

            var agent = await agentRepository.FirstOrDefaultAsync(a => a.ApiUserId == user.Id);
            if (agent == null)
            {
                return ServiceResult<LoginResult>.Failure("Er profil hittades ej, försök igen.");
            }

            string token = await GenerateToken(user);

            return ServiceResult<LoginResult>.SuccessResult(new LoginResult(token, user.Email!, user.Id, agent.Id));
        }

        public async Task<ServiceResult<RealEstateAgent>> RegisterAsync(AccountDTO dto)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = new ApiUser
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    UserName = dto.Email,
                    NormalizedUserName = dto.Email.ToUpper(),
                    NormalizedEmail = dto.Email.ToUpper(),
                    Email = dto.Email,
                };

                var result = await userManager.CreateAsync(user, dto.Password);

                if (!result.Succeeded)
                {
                    return ServiceResult<RealEstateAgent>.Failure(result.Errors.Select(e => new ServiceResultError { Code = e.Code, Description = e.Description}));
                }

                await userManager.AddToRoleAsync(user, ApiRoles.User);

                var agent = new RealEstateAgent
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    ImageUrl = dto.ImageUrl,
                    ApiUserId = user.Id,
                };

                var newAgent = await agentRepository.AddAsync(agent);

                if(newAgent == null)
                {
                    return ServiceResult<RealEstateAgent>.Failure("Lyckades inte skapa en Mäklare.");
                }

                await agencyService.GenerateApplication(dto, newAgent);

                await transaction.CommitAsync();
                return ServiceResult<RealEstateAgent>.SuccessResult(agent);

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ServiceResult<RealEstateAgent>.Failure(new ServiceResultError { Code = "Exception", Description = ex.Message });
            }
        }

        public async Task<bool> OwnedBy(ClaimsPrincipal user, int agentId)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId)) return false;

            var apiUser = await userManager.FindByEmailAsync(userId);
            if (apiUser == null) return false;

            var agent = await agentRepository.GetApiUserIdAsync(apiUser.Id);
            if (agent == null) return false;

            return agent.Id == agentId;
        }

        public async Task<bool> OwnedBy(ClaimsPrincipal user, List<int> ids)
        {
            foreach(var id in ids)
            {
                var res = await OwnedBy(user, id);
                if (res)
                {
                    return true;
                }
            }

            return false;
        }
        public async Task<ServiceResult<RealEstateAgent>> GetMyAgentAsync(ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                return ServiceResult<RealEstateAgent>.Failure("UserId saknas.");
            }

            var apiUser = await userManager.FindByEmailAsync(userId);
            if (apiUser == null)
            {
                return ServiceResult<RealEstateAgent>.Failure("Användare hittades ej." );
            }

            var agent = await agentRepository.GetApiUserIdAsync(apiUser.Id);
            if(agent == null)
            {
                return ServiceResult<RealEstateAgent>.Failure("Mäklare hittades ej.");
            }

            return ServiceResult<RealEstateAgent>.SuccessResult(agent);
        }
        public async Task UpdateAsync(UpdateAgentDTO dto, RealEstateAgent existingAgent)
        {

            mapper.Map(dto, existingAgent);

            dbContext.Agents.Update(existingAgent);
            
            await dbContext.SaveChangesAsync();
        }

        private async Task<string> GenerateToken(ApiUser apiUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[Settings.Key]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var roles = await userManager.GetRolesAsync(apiUser);
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();
            var userClaims = await userManager.GetClaimsAsync(apiUser);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, apiUser.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, apiUser.Email!),
                new Claim(ClaimTypes.NameIdentifier, apiUser.Id),
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
    }
}
