using FribergHomeAPI.Constants;
using FribergHomeAPI.Data.Repositories;
using FribergHomeAPI.DTOs;
using FribergHomeAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static FribergHomeAPI.Models.StatusTypes;

namespace FribergHomeAPI.Services
{
    //Author:Emelie
    public class AgencyService : IAgencyService
    {
        private readonly IRealEstateAgencyRepository agencyRepository;
        private readonly IRealEstateAgentRepository agentRepository;
        private readonly UserManager<ApiUser> userManager;

        public AgencyService(IRealEstateAgencyRepository agencyRepository, 
            IRealEstateAgentRepository agentRepository, 
            UserManager<ApiUser> userManager)
        {
            this.agencyRepository = agencyRepository;
            this.agentRepository = agentRepository;
            this.userManager = userManager;
        }
        public async Task<IActionResult> ApproveApplication(ApplicationDTO applicationDTO)
        {
            ChangeApplicationStatusAsync(applicationDTO);
            var agent = await agentRepository.GetByIdWithApiUser(applicationDTO.AgentId);
            SetAgency(applicationDTO.AgencyId, agent);
            ChangeUserRole(agent.ApiUser);

        }

        public async Task<IActionResult> DenyApplication(ApplicationDTO applicationDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> HandelApplication(ApplicationDTO applicationDTO)
        {
            if(applicationDTO.StatusType == StatusType.Approved)
            {
                ApproveApplication(applicationDTO);
                
            }
            else if(applicationDTO.StatusType == StatusType.Denied)
            {
                DenyApplication(applicationDTO);
            }
            else
            {
                throw new NotImplementedException(); //Think about what to return here
            }
        }

        public async void ChangeApplicationStatusAsync(ApplicationDTO applicationDTO)
        {
            var agency = await agencyRepository.GetByIdWithAgentsAsync(applicationDTO.AgencyId);
            var application = agency.Applications.FirstOrDefault(a => a.Id == applicationDTO.Id);
            application.StatusType = applicationDTO.StatusType;
            await agencyRepository.UpdateAsync(agency); //Does this work?
        }

        public async void ChangeUserRole(ApiUser apiUser)
        {
            await userManager.RemoveFromRoleAsync(apiUser, ApiRoles.User);
        }

        public async void SetAgency(int agencyId, RealEstateAgent agent)
        {
            agent.AgencyId = agencyId;
            await agentRepository.UpdateAsync(agent);
        }

    }
}
