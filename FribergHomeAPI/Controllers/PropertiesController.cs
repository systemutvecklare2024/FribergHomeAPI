using AutoMapper;
using FribergHomeAPI.Data.Repositories;
using FribergHomeAPI.DTOs;
using FribergHomeAPI.Models;
using FribergHomeAPI.Services;
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

        public PropertiesController(IPropertyRepository propertyRepo, 
			IMapper mapper, 
			UserManager<ApiUser> userManager,
            IRealEstateAgentRepository realEstateAgentRepository,
			IAccountService accountService)
		{
			this.propertyRepo = propertyRepo;
			this.mapper = mapper;
            this.accountService = accountService;
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
							.OrderByDescending(i=>i.Id)
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
		public async Task<IActionResult> Post([FromBody] PropertyDTO property)
		{
			try
			{
				var pro = mapper.Map<Property>(property);
				pro.RealEstateAgentId = 1; // TODO: FIX THIS SHIT (Emelie tm)
				var newProp = await propertyRepo.AddAsync(pro);
				if (newProp != null)
				{
					return Accepted();
				}

				return BadRequest(ModelState);

			}
			catch (Exception)
			{
				return Problem("aaaaaaaaa");
			}
		}

		// PUT api/<PropertiesController>/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] PropertyDTO dto)
		{
			// TODO: We need to validate that the user is allowed to do this...
			if (!ModelState.IsValid)
			{
				// Log all model errors
				foreach (var error in ModelState)
				{
					Console.WriteLine($"Key: {error.Key}");

                    foreach (var subError in error.Value.Errors)
                    {
                        Console.WriteLine($"  Error: {subError.ErrorMessage}");
                    }
                }
				// Fredrik
				return BadRequest(ModelState);
            }
			var existingProperty = await propertyRepo.GetWithAddressAsync(id);
			if (existingProperty == null) return NotFound();

			dto.Id = id;
			mapper.Map(dto, existingProperty);

            await propertyRepo.UpdateAsync(existingProperty);
            
            return Ok();
        }

		// DELETE api/<PropertiesController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
            // TODO: We need to validate that the user is allowed to do this...
        }

        // Author: Christoffer
        [HttpGet("{id}/details")]
		public async Task<IActionResult> GetAll(int id)
		{
            var property = await propertyRepo.GetWithAddressAndImages(id);

            var DTO = mapper.Map<PropertyDTO>(property);

            if (DTO == null)
            {
                return NotFound();
            }
            return Ok(DTO);
        }

		// Author: Christoffer
		[HttpGet("my")]
		public async Task<IActionResult> My()
		{
			var result = await accountService.GetMyAgentIdAsync(User);
			if (!result.Success)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(error.Code, error.Description);
				}
				return BadRequest(ModelState);
			}

			var properties = await propertyRepo.GetAllMyPropertiesAsync(result.Data);

			var dto = mapper.Map<List<PropertyDTO>>(properties);

			return Ok(dto);
		}
	}
}
