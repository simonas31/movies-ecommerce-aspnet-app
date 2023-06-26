using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.DTO;
using Server.Interfaces;
using Server.Models;
using Server.Repositories;

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

		[HttpGet("[controller]/{actorId}")]
		[ProducesResponseType(200, Type = typeof(Actor))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetActorAsync(string actorId)
		{
			if (!await _actorsRepository.ActorExistsByIdAsync(actorId))
				return NotFound();

			var actor = _mapper.Map<ActorDTO>(await _actorsRepository.GetActorAsync(actorId));

			if(!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(actor);
		}
		
		[HttpGet("[controller]s/{actorName}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Actor>))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetActorsAsync(string actorName)
		{
			if (!await _actorsRepository.ActorExistsAsync(actorName))
				return NotFound();

			var actors = _mapper.Map<List<ActorDTO>>(await _actorsRepository.GetByNameAsync(actorName));

			if(!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(actors);
		}

		[HttpGet("[controller]/Movies/{actorId}")]
		[ProducesResponseType(200, Type = typeof(Actor))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetActorMoviesAsync(string actorId)
		{
			var actorMovies = await _actorsRepository.GetActorMoviesAsync(actorId);

			if (actorMovies == null)
				return NotFound();
			else if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(actorMovies);
		}

		[HttpGet("[controller]s/Movies/{actorName}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Actor>))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetActorsMoviesAsync(string actorName)
		{
			var actorsMovies = await _actorsRepository.GetActorsMoviesAsync(actorName);

			if (actorsMovies == null)
				return NotFound();
			else if (actorsMovies.Count() == 0)
				return NotFound();
			else if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(actorsMovies);
		}
	}
}
