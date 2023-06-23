using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Server.DTO;
using Server.Interfaces;
using Server.Models;

namespace Server.Controllers
{
	[Route("api/[controller]")]
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

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Cinema>))]
		public async Task<IActionResult> GetAllCinemasAsync()
		{
			var cinemas = _mapper.Map<List<CinemaDTO>>(await _cinemasRepository.GetAllAsync());

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(cinemas);
		}

		[HttpGet("{name}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Actor>))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetCinemasAsync(string name)
		{
			if (!await _cinemasRepository.CinemaExistsAsync(name))
				return NotFound();

			var cinemas = _mapper.Map<List<CinemaDTO>>(await _cinemasRepository.GetByNameAsync(name));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(cinemas);
		}
	}
}
