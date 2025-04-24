using System.Threading.Tasks;
using AutoMapper;
using FribergHomeAPI.Data.Repositories;
using FribergHomeAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FribergHomeAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MuncipalityController : ControllerBase
	{
		private readonly IMuncipalityRepository muncipalityRepository;
		private readonly IMapper mapper;

		public MuncipalityController(IMuncipalityRepository muncipalityRepository, IMapper mapper)
		{
			this.muncipalityRepository = muncipalityRepository;
			this.mapper = mapper;
		}
		// GET: api/<MuncipalityController>
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var muncipalities = await muncipalityRepository.GetAllAsync();
			var dto = mapper.Map<List<MuncipalityDTO>>(muncipalities);

			if (dto == null)
			{
				return NotFound();
			}
			return Ok(dto);
		}

		// GET api/<MuncipalityController>/5
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var muncipality = await muncipalityRepository.GetByIdAsync(id);
			var dto = mapper.Map<MuncipalityDTO>(muncipality);
			if (dto == null)
			{
				return NotFound();
			}
			return Ok(dto);
		}
	}
}
