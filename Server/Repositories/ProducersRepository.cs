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
			var producers = await _context.Producers.ToListAsync();

			return producers.OrderBy(c => c.FullName).ToList();
		}

		public async Task<IEnumerable<Producer>> GetByNameAsync(string name)
		{
			var producers = await _context.Producers.ToListAsync();

			return producers.Where(c => c.FullName.Contains(name)).OrderBy(c => c.FullName).ToList();
		}

		public Task<bool> ProducerExistsAsync(string name)
		{
			return _context.Producers.AnyAsync(p => p.FullName.Contains(name));
		}
	}
}
