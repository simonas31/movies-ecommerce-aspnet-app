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
				.Where(c => c.FullName.Equals(name))
				.OrderBy(c => c.FullName).ToListAsync();
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
