using AutoMapper;
using FribergHomeAPI.Data.Repositories;
using FribergHomeAPI.DTOs;
using FribergHomeAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FribergHomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstateAgentsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRealEstateAgentRepository agentRepository;
        private readonly IAccountService accountservice;

        public RealEstateAgentsController(IMapper mapper, IRealEstateAgentRepository realEstateAgentRepository, IAccountService accountservice)
        {
            this.mapper = mapper;
            this.agentRepository = realEstateAgentRepository;
            this.accountservice = accountservice;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var agents = await agentRepository.GetAllAgentsAsync() ?? [];
            var dto = mapper.Map<List<RealEstateAgentDTO>>(agents);
            return Ok(dto);
        }
        //Tobias
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdWithAgency(int id)
        {
            var agent = await agentRepository.GetByIdWithAgencyAsync(id);
            var dto = mapper.Map<RealEstateAgentDTO>(agent);
            if(dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }
        
        //Tobias
        //To Do: Get id from logged in agent.
        [HttpGet("My")]
        public async Task<IActionResult> GetMyAgentWithAgency()
        {
            var result = await accountservice.GetMyAgentAsync(User);
            if (!result.Success)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            var dto = mapper.Map<RealEstateAgentDTO>(result);
            
            return Ok(dto);
        }

    }
}
