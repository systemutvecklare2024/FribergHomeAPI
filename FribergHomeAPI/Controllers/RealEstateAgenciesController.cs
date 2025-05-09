using AutoMapper;
using FribergHomeAPI.Data.Repositories;
using FribergHomeAPI.DTOs;
using FribergHomeAPI.Services;
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
        private readonly IAgencyService agencyService;

        public RealEstateAgenciesController(IMapper mapper, IRealEstateAgencyRepository agencyRepository, IAccountService accountservice, IAgencyService agencyService)
        {
            this.mapper = mapper;
            this.agencyRepository = agencyRepository;
            this.accountservice = accountservice;
            this.agencyService = agencyService;
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
            if (agency == null)
            {
                return NotFound();
            }

            var dto = mapper.Map<RealEstateAgencyPageDTO>(agency);
            
            return Ok(dto);
        }

        //Author:Emelie
        [HttpPost("{agencyId}/applications/{applicatonId}")]
        public async Task<IActionResult> HandleApplicationAsync(ApplicationDTO applicationDTO)
        {
            if (applicationDTO == null)
            {
                return BadRequest("Ansökan saknas, eller ogiltig.");
            }
            var result = await agencyService.HandleApplication(applicationDTO);
            if (!result.Success)
            {
                foreach (var error in result.Errors!)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem(ModelState);
            }
            return Ok();

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
    }
}
