using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.DTO;
using Server.Interfaces;
using Server.Models;
using System.Text.Json.Serialization;

namespace Server.Controllers
{
	[Route("api")]
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

		[HttpGet("[controller]s")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
		public async Task<IActionResult> GetAllMoviesAsync()
		{
			var movies = _mapper.Map<List<MovieDTO>>(await _moviesRepository.GetAllAsync());

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(movies);
		}

		[HttpGet("[controller]/{id}")]
		[ProducesResponseType(200, Type = typeof(Movie))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetMovie(string id)
		{
			if (!await _moviesRepository.MovieExistsByIdAsync(id))
				return NotFound();

			var movie = await _moviesRepository.GetMovieAsync(id);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(movie);
		}

		[HttpGet("[controller]s/{name}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetMoviesByNameAsync(string name)
		{
			if (!await _moviesRepository.MovieExistsAsync(name))
				return NotFound();

			var movies = _mapper.Map<List<MovieDTO>>(await _moviesRepository.GetByNameAsync(name));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(movies);
		}

		[HttpGet("[controller]s/Category/{category}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
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
	}
}
