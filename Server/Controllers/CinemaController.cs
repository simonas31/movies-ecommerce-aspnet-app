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

		[HttpGet("[controller]/{cinemaId}")]
		[ProducesResponseType(200, Type = typeof(Cinema))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetCinemaAsync(string cinemaId)
		{
			if (!await _cinemasRepository.CinemaExistsByIdAsync(cinemaId))
				return NotFound();

			var cinema = _mapper.Map<CinemaDTO>(await _cinemasRepository.GetCinemaAsync(cinemaId));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(cinema);
		}

		[HttpGet("[controller]s/{cinemaName}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Actor>))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetCinemasAsync(string cinemaName)
		{
			if (!await _cinemasRepository.CinemaExistsAsync(cinemaName))
				return NotFound();

			var cinemas = _mapper.Map<List<CinemaDTO>>(await _cinemasRepository.GetByNameAsync(cinemaName));

			if (cinemas.Count() <= 0)
				return NotFound();
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(cinemas);
		}

		[HttpGet("[controller]/Movies/{cinemaId}")]
		[ProducesResponseType(200, Type = typeof(Cinema))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetCinemaMoviesAsync(string cinemaId)
		{
			var cinemaMovies = await _cinemasRepository.GetCinemaMoviesAsync(cinemaId);

			if (cinemaMovies == null)
				return NotFound();
			else if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(cinemaMovies);
		}

		[HttpGet("[controller]s/Movies/{cinemaName}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Cinema>))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetCinemasMoviesAsync(string cinemaName)
		{
			var cinemasMovies = await _cinemasRepository.GetCinemasMoviesAsync(cinemaName);

			if (cinemasMovies == null)
				return NotFound();
			else if (cinemasMovies.Count() == 0)
				return NotFound();
			else if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(cinemasMovies);
		}
	}
}
