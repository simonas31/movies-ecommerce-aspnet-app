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
			return await _context.Cinemas.OrderBy(c => c.Name).ToListAsync();
		}

		public async Task<IEnumerable<Cinema>> GetByNameAsync(string name)
		{
			return await _context.Cinemas.Where(c => c.Name == name).ToListAsync();
		}

		public async Task<bool> CinemaExistsAsync(string name)
		{
			return await _context.Cinemas.AnyAsync(c => c.Name.Equals(name));
		}
	}
}
