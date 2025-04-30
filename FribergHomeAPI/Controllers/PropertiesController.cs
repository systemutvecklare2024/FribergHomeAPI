using AutoMapper;
using FribergHomeAPI.Data.Repositories;
using FribergHomeAPI.DTOs;
using FribergHomeAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FribergHome_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PropertiesController : ControllerBase
	{
		private readonly IPropertyRepository _propertyRepo;
		private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> userManager;
        private readonly IRealEstateAgentRepository realEstateAgentRepository;

        public PropertiesController(IPropertyRepository propertyRepo, 
			IMapper mapper, 
			UserManager<ApiUser> userManager,
            IRealEstateAgentRepository realEstateAgentRepository)
		{
			_propertyRepo = propertyRepo;
			_mapper = mapper;
            this.userManager = userManager;
            this.realEstateAgentRepository = realEstateAgentRepository;
        }

		// GET: api/<PropertiesController>

		[HttpGet]
		public async Task<ActionResult> Get()
		{
			var properties = await _propertyRepo.GetAllAsync() ?? [];
			var dto = _mapper.Map<List<PropertyDTO>>(properties);
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
			var property = await _propertyRepo.GetWithAddressAsync(id);

			var DTO = _mapper.Map<PropertyDTO>(property);

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
			var properties = await _propertyRepo.GetLatestAsync(take);

			if (properties == null)
			{
				return NotFound();
			}

			var DTO = _mapper.Map<List<PropertyDTO>>(properties)
							.OrderByDescending(i=>i.Id)
							.Take(take);

			return Ok(DTO);
		}

		// POST api/<PropertiesController>
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] PropertyDTO property)
		{
			try
			{
				var pro = _mapper.Map<Property>(property);
				pro.RealEstateAgentId = 1; // TODO: FIX THIS SHIT (Emelie tm)
				var newProp = await _propertyRepo.AddAsync(pro);
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
			var existingProperty = await _propertyRepo.GetWithAddressAsync(id);
			if (existingProperty == null) return NotFound();

			dto.Id = id;
			_mapper.Map(dto, existingProperty);

            await _propertyRepo.UpdateAsync(existingProperty);
            
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
            var property = await _propertyRepo.GetWithAddressAndImages(id);

            var DTO = _mapper.Map<PropertyDTO>(property);

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
            // This is awful, everything is awful. TODO: AccountService much?
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await userManager.FindByEmailAsync(userId);
            if (user == null) return Unauthorized();

            var agent = await realEstateAgentRepository.GetApiUserIdAsync(user.Id);
			if (agent == null) return Unauthorized();

			var properties = await _propertyRepo.GetAllMyPropertiesAsync(agent.Id);

			var dto = _mapper.Map<List<PropertyDTO>>(properties);

			return Ok(dto);
		}
	}
}
