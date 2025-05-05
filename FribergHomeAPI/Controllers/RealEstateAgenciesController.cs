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
    public class RealEstateAgenciesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRealEstateAgencyRepository agencyRepository;
        private readonly IAccountService accountservice;

        public RealEstateAgenciesController(IMapper mapper, IRealEstateAgencyRepository agencyRepository, IAccountService accountservice)
        {
            this.mapper = mapper;
            this.agencyRepository = agencyRepository;
            this.accountservice = accountservice;
        }

        //Tobias
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var agencies = await agencyRepository.GetAllAsync() ?? [];
            var dto = mapper.Map<List<RealEstateAgencyDTO>>(agencies);
            return Ok(dto);
        }
        //Tobias
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgencyByIdWithAgentsAsync(int id)
        {
            var agency = await agencyRepository.GetByIdWithAgentsAsync(id);
            var dto = mapper.Map<RealEstateAgencyPageDTO>(agency);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }
        [HttpGet("My")]
        public async Task<IActionResult> GetMyAgencyWithAgentsAsync()
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
            var agency = await agencyRepository.GetByIdWithAgentsAsync(result.Data.AgencyId.Value);

            var dto = mapper.Map<RealEstateAgencyPageDTO>(agency);

            return Ok(dto);
        }
        // To Do: SKapa en HttpGet /my som liknar den i agents.
    }
}
