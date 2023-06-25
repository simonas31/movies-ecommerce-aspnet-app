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

		public async Task<IEnumerable<Producer>> GetAllAsync()
		{
			return await _context.Producers.OrderBy(c => c.FullName).ToListAsync();
		}

		public async Task<IEnumerable<Producer>> GetByNameAsync(string name)
		{
			return await _context.Producers
				.Where(c => c.FullName.Contains(name))
				.OrderBy(c => c.FullName).ToListAsync();
		}

		public Task<bool> ProducerExistsAsync(string name)
		{
			return _context.Producers.AnyAsync(p => p.FullName.Contains(name));
		}
	}
}
