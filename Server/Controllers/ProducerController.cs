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
	public class ProducerController : Controller
	{
		private readonly IProducersRepository _producersRepository;
		private readonly IMapper _mapper;
		public ProducerController(IProducersRepository producersRepository, IMapper mapper)
		{
			_producersRepository = producersRepository;
			_mapper = mapper;
		}

		[HttpGet("[controller]s")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Producer>))]
		public async Task<IActionResult> GetAllProducersAsync()
		{
			var producers = _mapper.Map<List<ProducerDTO>>(await _producersRepository.GetAllAsync());

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(producers);
		}

		[HttpGet("[controller]/{producerId}")]
		[ProducesResponseType(200, Type = typeof(Producer))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetProducerAsync(string producerId)
		{
			if (!await _producersRepository.ProducerExistsByIdAsync(producerId))
				return NotFound();

			var producer = _mapper.Map<ProducerDTO>(await _producersRepository.GetProducerAsync(producerId));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(producer);
		}

		[HttpGet("[controller]s/{producerName}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Producer>))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetProducersAsync(string producerName)
		{
			if (!await _producersRepository.ProducerExistsAsync(producerName))
				return NotFound();

			var producers = _mapper.Map<List<ProducerDTO>>(await _producersRepository.GetByNameAsync(producerName));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(producers);
		}

		[HttpGet("[controller]/Movies/{producerId}")]
		[ProducesResponseType(200, Type = typeof(Producer))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetProducerMoviesAsync(string producerId)
		{
			var producerMovies = await _producersRepository.GetProducerMoviesAsync(producerId);

			if (producerMovies == null)
				return NotFound();
			else if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(producerMovies);
		}

		[HttpGet("[controller]s/Movies/{producerName}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Producer>))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetProducersMoviesAsync(string producerName)
		{
			var movies = await _producersRepository.GetProducersMoviesAsync(producerName);

			if (movies == null)
				return NotFound();
			else if (movies.Count() == 0)
				return NotFound();
			else if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(movies);
		}
	}
}
