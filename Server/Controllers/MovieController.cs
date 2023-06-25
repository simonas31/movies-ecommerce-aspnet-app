using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.DTO;
using Server.Interfaces;
using Server.Models;

namespace Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MovieController : Controller
	{
		private readonly IMoviesRepository _moviesRepository;
		private readonly IMapper _mapper;
		public MovieController(IMoviesRepository moviesRepository, IMapper mapper)
		{
			_moviesRepository = moviesRepository;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<MovieDTO>))]
		public async Task<IActionResult> GetAllMoviesAsync()
		{
			var movies = _mapper.Map<List<MovieDTO>>(await _moviesRepository.GetAllAsync());

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(movies);
		}

		[HttpGet("{name}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<MovieDTO>))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetMoviesAsync(string name)
		{
			if (!await _moviesRepository.MovieExistsAsync(name))
				return NotFound();

			var movies = _mapper.Map<List<MovieDTO>>(await _moviesRepository.GetByNameAsync(name));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(movies);
		}

		[HttpGet("Category/{category}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<MovieDTO>))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetByCategoryAsync(string category)
		{
			if (!await _moviesRepository.MovieExistsByCategoryAsync(category))
				return NotFound();

			var movies = _mapper.Map<List<MovieDTO>>(await _moviesRepository.GetByCategoryAsync(category));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(movies);
		}

		[HttpGet("Cinema/{cinemaName}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetCinemaMoviesAsync(string cinemaName)
		{
			var movies = _mapper.Map<List<MovieDTO>>(await _moviesRepository.GetCinemaMoviesAsync(cinemaName));

			if(movies == null)
				return NotFound();
			else if (movies.Count() == 0)
				return NotFound();
			else if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(movies);
		}
	}
}
