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
	public class CinemaController : Controller
	{
		private readonly ICinemasRepository _cinemasRepository;
		private readonly IMapper _mapper;
		public CinemaController(ICinemasRepository cinemasRepository, IMapper mapper)
		{
			_cinemasRepository = cinemasRepository;
			_mapper = mapper;
		}

		[HttpGet("[controller]s")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Cinema>))]
		public async Task<IActionResult> GetAllCinemasAsync()
		{
			var cinemas = _mapper.Map<List<CinemaDTO>>(await _cinemasRepository.GetAllAsync());

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(cinemas);
		}

		[HttpGet("[controller]/{id}")]
		[ProducesResponseType(200, Type = typeof(Cinema))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetActorAsync(string id)
		{
			if (!await _cinemasRepository.CinemaExistsByIdAsync(id))
				return NotFound();

			var cinema = await _cinemasRepository.GetCinemaAsync(id);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(cinema);
		}

		[HttpGet("[controller]s/{name}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Actor>))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetCinemasAsync(string name)
		{
			if (!await _cinemasRepository.CinemaExistsAsync(name))
				return NotFound();

			var cinemas = _mapper.Map<List<CinemaDTO>>(await _cinemasRepository.GetByNameAsync(name));

			if (cinemas.Count() <= 0)
				return NotFound();
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(cinemas);
		}
	}
}
