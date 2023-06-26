using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Interfaces;
using Server.Models;

namespace Server.Repositories
{
	public class ProducersRepository : IProducersRepository
	{
		private readonly ApplicationDbContext _context;
		public ProducersRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Gets all producers
		/// </summary>
		/// <returns>producers list</returns>
		public async Task<ICollection<Producer>> GetAllAsync()
		{
			return await _context.Producers.OrderBy(c => c.FullName).ToListAsync();
		}

		/// <summary>
		/// Gets producer by id
		/// </summary>
		/// <param name="id">producer id</param>
		/// <returns>producer or null</returns>
		public async Task<Producer> GetProducerAsync(string id)
		{
			return await _context.Producers.Where(p => p.Id.Equals(id)).FirstOrDefaultAsync();
		}

		/// <summary>
		/// Gets producers by name
		/// </summary>
		/// <param name="name">producer name</param>
		/// <returns>producers list</returns>
		public async Task<ICollection<Producer>> GetByNameAsync(string name)
		{
			return await _context.Producers
				.Where(c => c.FullName.Contains(name))
				.OrderBy(c => c.FullName).ToListAsync();
		}

		/// <summary>
		/// Gets producer(-s) movies, by producer name
		/// </summary>
		/// <param name="producerName">producer name</param>
		/// <returns>producers movies list</returns>
		public async Task<ICollection<Producer>> GetProducersMoviesAsync(string producerName)
		{
			if (!await ProducerExistsAsync(producerName))
			{
				return null;
			}

			//get producers that contains producer name
			var producers = await _context.Producers
				.Where(p => p.FullName.Contains(producerName))
				.Select(p => new Producer() { Id = p.Id, ProfilePicture = p.ProfilePicture, FullName = p.FullName, Description = p.Description, Producer_Movies = p.Producer_Movies })
				.ToListAsync();

			//get movies that contains found producers from movies table
			var producersMovies = (from m in _context.Movies.ToList()
						  where producers.Count() > 0
						  from p in producers
						  where p.Producer_Movies != null && p.Producer_Movies.Count > 0
						  from pm in p.Producer_Movies
						  where pm.MovieId.Equals(m.Id)
						  select new { p, m }).ToList().DistinctBy(c => c.p.Id);

			return producersMovies.Select(o => o.p).ToList();
		}

		/// <summary>
		/// Gets producer movies, by producer id
		/// </summary>
		/// <param name="producerId">producer id</param>
		/// <returns>Producer movies list</returns>
		public async Task<Producer> GetProducerMoviesAsync(string producerId)
		{
			if (!await ProducerExistsByIdAsync(producerId))
			{
				return null;
			}

			var producer = await _context.Producers
				.Where(p => p.Id.Equals(producerId))
				.Select(p => new Producer { Id=p.Id, ProfilePicture=p.ProfilePicture, FullName=p.FullName, Description=p.Description, Producer_Movies=p.Producer_Movies })
				.FirstOrDefaultAsync();

			var producerMovies = (from m in await _context.Movies.ToListAsync()
								  where producer != null
						  from pm in producer.Producer_Movies
						  where pm.MovieId.Equals(m.Id)
						  select producer).FirstOrDefault();

			return producerMovies;
		}

		/// <summary>
		/// Checks if producer exsist by producer name
		/// </summary>
		/// <param name="name">producer name</param>
		/// <returns>true or false</returns>
		public async Task<bool> ProducerExistsAsync(string name)
		{
			return await _context.Producers.AnyAsync(p => p.FullName.Contains(name));
		}

		/// <summary>
		/// Checks if producer exsist by producer id
		/// </summary>
		/// <param name="name">producer id</param>
		/// <returns>true or false</returns>
		public async Task<bool> ProducerExistsByIdAsync(string id)
		{
			return await _context.Producers.AnyAsync(p => p.Id.Equals(id));
		}
	}
}
