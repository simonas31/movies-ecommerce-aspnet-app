using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.DTO;
using Server.Interfaces;
using Server.Models;

namespace Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ActorController : Controller
	{
		private readonly IActorsRepository _actorsRepository;
		private readonly IMapper _mapper;
		public ActorController(IActorsRepository actorsRepository, IMapper mapper)
		{
			_actorsRepository = actorsRepository;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Actor>))]
		public async Task<IActionResult> GetAllActorsAsync()
		{
			var actors = _mapper.Map<List<ActorDTO>>(await _actorsRepository.GetAllAsync());

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(actors);
		}

		[HttpGet("{name}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Actor>))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetActorsAsync(string name)
		{
			if (!await _actorsRepository.ActorExistsAsync(name))
				return NotFound();

			var actors = _mapper.Map<List<ActorDTO>>(await _actorsRepository.GetByNameAsync(name));

			if(!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(actors);
		}
	}
}
