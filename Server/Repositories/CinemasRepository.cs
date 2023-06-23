using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Interfaces;
using Server.Models;

namespace Server.Repositories
{
	public class CinemasRepository : ICinemasRepository
	{
		private readonly ApplicationDbContext _context;
		public CinemasRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Cinema>> GetAllAsync()
		{
			var cinemas = await _context.Cinemas.ToListAsync();

			return cinemas.OrderBy(c => c.Name).ToList();
		}

		public async Task<IEnumerable<Cinema>> GetByNameAsync(string name)
		{
			var cinemas = await _context.Cinemas.ToListAsync();

			return cinemas.Where(c => c.Name == name).ToList();
		}

		public Task<bool> CinemaExistsAsync(string name)
		{
			return _context.Cinemas.AnyAsync(c => c.Name.Contains(name));
		}
	}
}
