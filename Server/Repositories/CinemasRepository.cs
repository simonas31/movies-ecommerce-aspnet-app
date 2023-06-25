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

		/// <summary>
		/// Gets all cinemas
		/// </summary>
		/// <returns>Cinemas list</returns>
		public async Task<ICollection<Cinema>> GetAllAsync()
		{
			return await _context.Cinemas.OrderBy(c => c.Name).ToListAsync();
		}

		/// <summary>
		/// Gets cinema by id
		/// </summary>
		/// <param name="id">cinema id</param>
		/// <returns>cinema or null</returns>
		public async Task<Cinema> GetCinemaAsync(string id)
		{
			return await _context.Cinemas.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
		}

		/// <summary>
		/// Gets cinemas by name
		/// </summary>
		/// <param name="name">cinema name</param>
		/// <returns>cinemas list</returns>
		public async Task<ICollection<Cinema>> GetByNameAsync(string name)
		{
			return await _context.Cinemas.Where(c => c.Name.Contains(name)).ToListAsync();
		}

		/// <summary>
		/// Checks if cinema exsist by cinema name
		/// </summary>
		/// <param name="name">cinema name</param>
		/// <returns>true or false</returns>
		public async Task<bool> CinemaExistsAsync(string name)
		{
			return await _context.Cinemas.AnyAsync(c => c.Name.Contains(name));
		}

		/// <summary>
		/// Checks if cinema exsist by cinema id
		/// </summary>
		/// <param name="id">cinema id</param>
		/// <returns>true or false</returns>
		public async Task<bool> CinemaExistsByIdAsync(string id)
		{
			return await _context.Cinemas.AnyAsync(c => c.Id.Equals(id));
		}
	}
}
