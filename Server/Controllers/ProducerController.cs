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

		[HttpGet("[controller]/{id}")]
		[ProducesResponseType(200, Type = typeof(Producer))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetActorAsync(string id)
		{
			if (!await _producersRepository.ProducerExistsByIdAsync(id))
				return NotFound();

			var producer = await _producersRepository.GetProducerAsync(id);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(producer);
		}

		[HttpGet("[controller]s/{name}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Producer>))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetProducersAsync(string name)
		{
			if (!await _producersRepository.ProducerExistsAsync(name))
				return NotFound();

			var producers = _mapper.Map<List<ProducerDTO>>(await _producersRepository.GetByNameAsync(name));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(producers);
		}
	}
}
