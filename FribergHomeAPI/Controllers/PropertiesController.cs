using AutoMapper;
using FribergHomeAPI.Data.Repositories;
using FribergHomeAPI.DTOs;
using FribergHomeAPI.Models;
using FribergHomeAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FribergHome_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PropertiesController : ControllerBase
	{
		private readonly IPropertyRepository propertyRepo;
		private readonly IMapper mapper;
		private readonly IAccountService accountService;
		private readonly IPropertyService propertyService;

		public PropertiesController(IPropertyRepository propertyRepo,
			IMapper mapper,
			UserManager<ApiUser> userManager,
			IRealEstateAgentRepository realEstateAgentRepository,
			IAccountService accountService,
			IPropertyService propertyService)
		{
			this.propertyRepo = propertyRepo;
			this.mapper = mapper;
			this.accountService = accountService;
			this.propertyService = propertyService;
		}

		// GET: api/<PropertiesController>
		[HttpGet]
		public async Task<ActionResult> Get()
		{
			var properties = await propertyRepo.GetAllAsync() ?? [];
			var dto = mapper.Map<List<PropertyDTO>>(properties);
			if (dto == null)
			{
				return NotFound();
			}
			return Ok(dto);
		}

		// GET api/<PropertiesController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult> Get(int id)
		{
			var property = await propertyRepo.GetWithAddressAsync(id);

			var DTO = mapper.Map<PropertyDTO>(property);

			if (DTO == null)
			{
				return NotFound();
			}
			return Ok(DTO);
		}

		//Author: Glate
		[HttpGet("latest")]
		public async Task<ActionResult> GetLatest(int take = 5) //Defaults take to 5 if no query is passed
		{
			var properties = await propertyRepo.GetLatestAsync(take);

			if (properties == null)
			{
				return NotFound();
			}

			var DTO = mapper.Map<List<PropertyDTO>>(properties)
							.OrderByDescending(i => i.Id)
							.Take(take);

			return Ok(DTO);
		}

		[HttpGet("muncipality/{muncipalityId}")]
		public async Task<ActionResult> GetByMuncipality(int muncipalityId)
		{
			var properties = await propertyRepo.GetByMuncipalityId(muncipalityId);

			if (properties == null)
			{
				return NotFound();
			}

			var DTO = mapper.Map<List<PropertyDTO>>(properties)
							 .OrderByDescending(i => i.Id);

			return Ok(DTO);
		}

		// POST api/<PropertiesController>
		[HttpPost]
		[Authorize(Roles = "Agent, SuperAgent")]
		public async Task<IActionResult> Post([FromBody] PropertyDTO property)
		{
			try
			{
				var pro = mapper.Map<Property>(property);
				var agentId = await accountService.GetMyAgentAsync(User);

				if (agentId == null) //If no agent, no adding properties right now.
				{
					return BadRequest(ModelState);
				}
				pro.RealEstateAgentId = agentId.Data!.Id; //FIXED THIS SHIT (GLATE tm)
				var newProp = await propertyRepo.AddAsync(pro);
				if (newProp != null)
				{
					return Accepted();
				}

				return BadRequest(ModelState);

			}
			catch (Exception ex)
			{
				return Problem(detail: ex.Message, statusCode: 500);
			}
		}

        //Author: Glate, Fredrik
        // PUT api/<PropertiesController>/5
        [HttpPut("{id}")]
		[Authorize(Roles = "Agent, SuperAgent")]
		public async Task<IActionResult> Put(int id, [FromBody] PropertyDTO dto)
		{
			var result = await propertyService.UpdatePropertyAsync(User, id, dto);
			if (!result.Success)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(error.Code, error.Description);
				}
				return BadRequest(ModelState);
			}
			return Ok(result.Success);
		}

		// Author: Christoffer
		// DELETE api/<PropertiesController>/5
		[HttpDelete("{id}")]
		[Authorize( Roles = "Agent, SuperAgent")]
		public async Task<IActionResult> Delete(int id)
        {
			var res = await propertyService.DeleteAsync(User, id);
			if(!res.Success)
			{
				var errorCode = Convert.ToInt32(res?.Errors?.FirstOrDefault()?.Code ?? "400");

                return StatusCode( errorCode, res?.Errors.FirstOrDefault());
			}

			return Ok();
        }

		// Author: Christoffer
		[HttpGet("{id}/details")]
		public async Task<IActionResult> GetAll(int id)
		{
			var property = await propertyRepo.GetWithAddressImagesAndMuncipality(id);

			var DTO = mapper.Map<PropertyDTO>(property);

			if (DTO == null)
			{
				return NotFound();
			}
			return Ok(DTO);
		}

		// Author: Christoffer
		[HttpGet("my")]
		[Authorize(Roles = "Agent, SuperAgent")]
		public async Task<IActionResult> My()
		{
			var result = await accountService.GetMyAgentAsync(User);
			if (!result.Success)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(error.Code, error.Description);
				}
				return BadRequest(ModelState);
			}

			var properties = await propertyRepo.GetAllPropertiesByAgentIdAsync(result.Data.Id);

			var dto = mapper.Map<List<PropertyDTO>>(properties);

			return Ok(dto);
		}
	}
}
