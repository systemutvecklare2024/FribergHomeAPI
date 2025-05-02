using AutoMapper;
using FribergHomeAPI.Data.Repositories;
using FribergHomeAPI.DTOs;
using FribergHomeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FribergHomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstateAgenciesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRealEstateAgencyRepository agencyRepository;

        public RealEstateAgenciesController(IMapper mapper, IRealEstateAgencyRepository agencyRepository)
        {
            this.mapper = mapper;
            this.agencyRepository = agencyRepository;
        }

        //Tobias
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var agencies = await agencyRepository.GetAllAsync() ?? [];
            var dto = mapper.Map<List<RealEstateAgencyDTO>>(agencies);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWithAll(int id)
        {
            var agency = await agencyRepository.GetWithApplicationsAndAgentsAsync(id);
            var agentDTO = mapper.Map<RealEstateAgency>(agency);
            return Ok(agentDTO);
        }
    }
}
