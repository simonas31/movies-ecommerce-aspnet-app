using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.DTO;
using Server.Interfaces;
using Server.Models;

namespace Server.Controllers
{
	[Route("api")]
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

		[HttpGet("[controller]s")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Actor>))]
		public async Task<IActionResult> GetAllActorsAsync()
		{
			var actors = _mapper.Map<List<ActorDTO>>(await _actorsRepository.GetAllAsync());

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(actors);
		}

		[HttpGet("[controller]/{id}")]
		[ProducesResponseType(200, Type = typeof(Actor))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetActorAsync(string id)
		{
			if (!await _actorsRepository.ActorExistsByIdAsync(id))
				return NotFound();

			var actor = await _actorsRepository.GetActorAsync(id);

			if(!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(actor);
		}
		
		[HttpGet("[controller]s/{name}")]
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
