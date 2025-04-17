using AutoMapper;
using FribergHomeAPI.Data.Repositories;
using FribergHomeAPI.DTOs;
using FribergHomeAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FribergHome_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PropertiesController : ControllerBase
	{
		private readonly IPropertyRepository _propertyRepo;
		private readonly IMapper _mapper;

		public PropertiesController(IPropertyRepository propertyRepo, IMapper mapper)
		{
			 _propertyRepo = propertyRepo;
			_mapper = mapper;
		}

		// GET: api/<PropertiesController>
		[HttpGet]
		public async Task<ActionResult> Get()
		{
			var properties =  await _propertyRepo.GetAllAsync() ?? [];
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

			} catch (Exception)
			{
				return Problem("aaaaaaaaa");
			}
		}

		// PUT api/<PropertiesController>/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] PropertyDTO property)
		{
			if(!ModelState.IsValid)
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
            }
			Console.WriteLine("I GOT IT!");
			Console.WriteLine(property);

			return Accepted();
		}

		// DELETE api/<PropertiesController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
