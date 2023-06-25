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

		/// <summary>
		/// Gets all actors
		/// </summary>
		/// <returns>actors list</returns>
		public async Task<ICollection<Actor>> GetAllAsync()
		{
			return await _context.Actors.OrderBy(c => c.FullName).ToListAsync();
		}

		/// <summary>
		/// Gets actor by actor id
		/// </summary>
		/// <param name="id">actor id</param>
		/// <returns>actor or null</returns>
		public async Task<Actor> GetActorAsync(string id)
		{
			return await _context.Actors.Where(a => a.Id.Equals(id)).FirstOrDefaultAsync();
		}
		
		/// <summary>
		/// Gets actors by name
		/// </summary>
		/// <param name="name">actor name</param>
		/// <returns>actors list</returns>
		public async Task<ICollection<Actor>> GetByNameAsync(string name)
		{
			return await _context.Actors
				.Where(a => a.FullName.Contains(name))
				.OrderBy(a => a.FullName).ToListAsync();
		}

		/// <summary>
		/// Checks if actor exsist by actor name
		/// </summary>
		/// <param name="name">actor name</param>
		/// <returns>true or false</returns>
		public async Task<bool> ActorExistsAsync(string name)
		{
			return await _context.Actors.AnyAsync(a => a.FullName.Contains(name));
		}

		/// <summary>
		/// Checks if actor exsist by actor id
		/// </summary>
		/// <param name="id">actor id</param>
		/// <returns>true or false</returns>
		public async Task<bool> ActorExistsByIdAsync(string id)
		{
			return await _context.Actors.AnyAsync(a => a.Id.Equals(id));
		}
	}
}
