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
			var actors = await _context.Actors.ToListAsync();

			return actors.OrderBy(c => c.FullName).ToList();
		}

		public async Task<IEnumerable<Actor>> GetByNameAsync(string name)
		{
			var actors = await _context.Actors.ToListAsync();

			return actors.Where(a => a.FullName.Contains(name)).OrderBy(a => a.FullName).ToList();
		}

		public Task<bool> ActorExistsAsync(string name)
		{
			return _context.Actors.AnyAsync(a => a.FullName.Contains(name));
		}
	}
}
