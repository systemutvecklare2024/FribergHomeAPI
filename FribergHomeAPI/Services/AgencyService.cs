using FribergHomeAPI.Constants;
using FribergHomeAPI.Data;
using FribergHomeAPI.Data.Repositories;
using FribergHomeAPI.DTOs;
using FribergHomeAPI.Models;
using FribergHomeAPI.Results;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using static FribergHomeAPI.Models.StatusTypes;

namespace FribergHomeAPI.Services
{
    //Author:Emelie
    public class AgencyService : IAgencyService
    {
        private readonly IRealEstateAgencyRepository agencyRepository;
        private readonly IRealEstateAgentRepository agentRepository;
        private readonly UserManager<ApiUser> userManager;
        private readonly ApplicationDbContext dbContext;
        private readonly IAccountService accountService;

        public AgencyService(IRealEstateAgencyRepository agencyRepository, 
            IRealEstateAgentRepository agentRepository, 
            UserManager<ApiUser> userManager,
            ApplicationDbContext dbContext,
            IAccountService accountService)
        {
            this.agencyRepository = agencyRepository;
            this.agentRepository = agentRepository;
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.accountService = accountService;
        }

        public async Task<ServiceResult> HandleApplication(ClaimsPrincipal user, ApplicationDTO applicationDTO)
        {
            var agency = await agencyRepository.GetByIdWithAgentsAsync(applicationDTO.AgencyId);
            if (agency == null || agency.Applications == null)
            {
                return ServiceResult.Failure("Byrå eller ansökningar saknas");
            }

            var agentIds = agency?.Agents?.Select(e => e.Id).ToList() ?? [];
            var isAllowed = await accountService.IsAllowed(user, agentIds);
            if(!isAllowed)
            {
                return ServiceResult.Failure(new ServiceResultError { Code = StatusCodes.Status403Forbidden.ToString(), Description = "Ni saknar rättigheter att hantera mäklarbyrån." });
            }

            return applicationDTO.StatusType switch
            {
                StatusType.Approved => await HandleApprovedAsync(applicationDTO),
                StatusType.Denied => await HandleDeniedAsync(applicationDTO),
                _ => ServiceResult.Failure(new ServiceResultError { Code = StatusCodes.Status400BadRequest.ToString(), Description = "Ogiltig status på ansökan, ingen förändring utförd."})
            };
        }

        private async Task<ServiceResult> HandleApprovedAsync(ApplicationDTO applicationDTO)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var result = await UpdateApplicationStatusAsync(applicationDTO);
                if (!result.Success)
                {
                    return result;
                }

                var agent = await agentRepository.GetByIdWithApiUser(applicationDTO.AgentId);
                if (agent == null)
                {
                    return ServiceResult.Failure("Ingen mäklare funnen");
                }

                await SetAgency(applicationDTO.AgencyId, agent);

                var identityResult = await ChangeUserRole(agent.ApiUser);
                if (!identityResult.Succeeded)
                {
                    return ServiceResult.Failure(identityResult.Errors.Select(e => new ServiceResultError { Code = e.Code, Description = e.Description }));
                }

                await transaction.CommitAsync();

                return ServiceResult.SuccessResult();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ServiceResult.Failure(new ServiceResultError { Code = StatusCodes.Status500InternalServerError.ToString(), Description = ex.Message });
            }
        }

        private async Task<ServiceResult> HandleDeniedAsync(ApplicationDTO applicationDTO)
        {
            return await UpdateApplicationStatusAsync(applicationDTO);
        }
        private async Task<ServiceResult> UpdateApplicationStatusAsync(ApplicationDTO applicationDTO)
        {
            var agency = await agencyRepository.GetByIdWithAgentsAsync(applicationDTO.AgencyId);
            if(agency == null)
            {
                return ServiceResult.Failure(new ServiceResultError { Code = StatusCodes.Status404NotFound.ToString(), Description = "Byrån hittades ej." });
            }

            var application = agency.Applications.FirstOrDefault(a => a.Id == applicationDTO.Id);
            if(application == null)
            {
                return ServiceResult.Failure(new ServiceResultError { Code = StatusCodes.Status404NotFound.ToString(), Description = "Ansökan hittades ej." });
            }
            application.StatusType = applicationDTO.StatusType;
            await agencyRepository.UpdateAsync(agency);

            return ServiceResult.SuccessResult();
        }

        private async Task<IdentityResult> ChangeUserRole(ApiUser apiUser)
        {
            var removeResult = await userManager.RemoveFromRoleAsync(apiUser, ApiRoles.User);
            if (!removeResult.Succeeded)
            {
                return removeResult;
            }

            var addResult = await userManager.AddToRoleAsync(apiUser, ApiRoles.Agent);
            if (!addResult.Succeeded)
            {
                return addResult;
            }

            return IdentityResult.Success;
        }

        private async Task SetAgency(int agencyId, RealEstateAgent agent)
        {
            agent.AgencyId = agencyId;
            await agentRepository.UpdateAsync(agent);
        }
    }
}
