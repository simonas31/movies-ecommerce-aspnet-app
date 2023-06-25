using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Interfaces;
using Server.Models;

namespace Server.Repositories
{
	public class ActorsRepository : IActorsRepository
	{
		private readonly ApplicationDbContext _context;
		public ActorsRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Actor>> GetAllAsync()
		{
			return await _context.Actors.OrderBy(c => c.FullName).ToListAsync();
		}

		public async Task<IEnumerable<Actor>> GetByNameAsync(string name)
		{
			return await _context.Actors
				.Where(a => a.FullName.Equals(name))
				.OrderBy(a => a.FullName).ToListAsync();
		}

		public Task<bool> ActorExistsAsync(string name)
		{
			return _context.Actors.AnyAsync(a => a.FullName.Equals(name));
		}
	}
}
