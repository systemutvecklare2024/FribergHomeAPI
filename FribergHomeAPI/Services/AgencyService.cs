using FribergHomeAPI.Constants;
using FribergHomeAPI.Data;
using FribergHomeAPI.Data.Repositories;
using FribergHomeAPI.DTOs;
using FribergHomeAPI.Models;
using FribergHomeAPI.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public AgencyService(IRealEstateAgencyRepository agencyRepository, 
            IRealEstateAgentRepository agentRepository, 
            UserManager<ApiUser> userManager,
            ApplicationDbContext dbContext)
        {
            this.agencyRepository = agencyRepository;
            this.agentRepository = agentRepository;
            this.userManager = userManager;
            this.dbContext = dbContext;
        }
        public async Task<ServiceResult<bool>> ApproveApplication(ApplicationDTO applicationDTO)
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var result = await ChangeApplicationStatusAsync(applicationDTO);
                if (!result.Success)
                {
                    return result;
                }
                var agent = await agentRepository.GetByIdWithApiUser(applicationDTO.AgentId);
                if(agent == null)
                {
                    return ServiceResult<bool>.Failure("Ingen mäklare funnen");
                }
                await SetAgency(applicationDTO.AgencyId, agent);
                var identityResult = await ChangeUserRole(agent.ApiUser);
                if (!identityResult.Succeeded)
                {
                    return ServiceResult<bool>.Failure(identityResult.Errors.Select(e => new ServiceResultError { Code = e.Code, Description = e.Description }));
                }

                await transaction.CommitAsync();

                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ServiceResult<bool>.Failure(new ServiceResultError { Code = "Exception", Description = ex.Message });
            }
            
        }

        public async Task<ServiceResult<bool>> DenyApplication(ApplicationDTO applicationDTO)
        {
            await ChangeApplicationStatusAsync(applicationDTO);
            return ServiceResult<bool>.SuccessResult(true);
        }

        public async Task<ServiceResult<bool>> HandleApplication(ApplicationDTO applicationDTO)
        {
            if(applicationDTO.StatusType == StatusType.Approved)
            {
                var result = ApproveApplication(applicationDTO);
                return await result;
            }
            else if(applicationDTO.StatusType == StatusType.Denied)
            {
                var result = DenyApplication(applicationDTO);
                return await result;
            }
            else
            {
                return ServiceResult<bool>.Failure("Något gick fel, ingen förändring utförd på status på ansökan.");
            }
        }

        public async Task<ServiceResult<bool>> ChangeApplicationStatusAsync(ApplicationDTO applicationDTO)
        {
            var agency = await agencyRepository.GetByIdWithAgentsAsync(applicationDTO.AgencyId);
            if(agency == null || agency.Applications == null)
            {
                return ServiceResult<bool>.Failure("Byrå eller ansökningar saknas");
            }
            var application = agency.Applications.FirstOrDefault(a => a.Id == applicationDTO.Id);
            if(application == null)
            {
                return ServiceResult<bool>.Failure("Ansökningen hittades inte");
            }
            application.StatusType = applicationDTO.StatusType;
            await agencyRepository.UpdateAsync(agency);

            return ServiceResult<bool>.SuccessResult(true);
        }

        public async Task<IdentityResult> ChangeUserRole(ApiUser apiUser)
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

        public async Task SetAgency(int agencyId, RealEstateAgent agent)
        {
            agent.AgencyId = agencyId;
            await agentRepository.UpdateAsync(agent);
        }

        public async Task GenerateApplication(AccountDTO accountDTO, RealEstateAgent agent) //Return ServiceResult?
        {
            var application = new Application()
            {
                AgencyId = accountDTO.AgencyId,
                AgentId = agent.Id
            };
            var agency = await agencyRepository.GetByIdWithAgentsAsync(application.AgencyId);
            agency.Applications.Add(application);
            await agencyRepository.UpdateAsync(agency);
        }

    }
}
