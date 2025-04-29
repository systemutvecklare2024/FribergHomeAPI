using AutoMapper;
using FribergHomeAPI.Data.Repositories;
using FribergHomeAPI.DTOs;
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

        public RealEstateAgentsController(IMapper mapper, IRealEstateAgentRepository realEstateAgentRepository)
        {
            this.mapper = mapper;
            this.agentRepository = realEstateAgentRepository;
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
            return Ok(dto);
        }
        
        //Tobias
        //To Do: Get id from logged in agent.
        [HttpGet("My/{userId}")]
        public async Task<IActionResult> GetMyIdWithAgency(string userId)
        {
            var agentId = userId;
            var agent = await agentRepository.GetApiUserIdAsync(agentId);
            var dto = mapper.Map<RealEstateAgentDTO>(agent);
            return Ok(dto);
        }

    }
}
