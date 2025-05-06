using AutoMapper;
using FribergHomeAPI.Data.Repositories;
using FribergHomeAPI.DTOs;
using FribergHomeAPI.Models;
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
        private readonly IAgencyService agencyService;

        public RealEstateAgenciesController(IMapper mapper, IRealEstateAgencyRepository agencyRepository, IAgencyService agencyService)
        {
            this.mapper = mapper;
            this.agencyRepository = agencyRepository;
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
            var dto = mapper.Map<RealEstateAgencyPageDTO>(agency);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        //Author:Emelie
        [HttpPost("{agencyId}/applications/{applicatonId}")]
        public async Task<IActionResult> HandleApplicationAsync(ApplicationDTO applicationDTO)
        {
            if (applicationDTO == null)
            {
                return BadRequest("Hittar inte ansökan"); //Change to better SatusCode???
            }
            var result = await agencyService.HandelApplication(applicationDTO);
            if (!result.Success)
            {
                foreach (var error in result.Errors!)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }
            return Ok();

        }
    }
}
