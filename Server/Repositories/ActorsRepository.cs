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
		/// Gets actor(-s) movies, by actor name
		/// </summary>
		/// <param name="actorName">actor name</param>
		/// <returns>actors movies list</returns>
		public async Task<ICollection<Actor>> GetActorsMoviesAsync(string actorName)
		{
			if (!await ActorExistsAsync(actorName))
			{
				return null;
			}

			//get actors that contains actorName
			var actors = await _context.Actors
				.Where(a => a.FullName.Contains(actorName))
				.Select(a => new Actor() { Id = a.Id, ProfilePicture = a.ProfilePicture, FullName = a.FullName, Bio = a.Bio, Actor_Movies = a.Actor_Movies })
				.ToListAsync();

			//get movies that contains found actor from movies table
			var actorsMovies = (from m in _context.Movies.ToList()
						  where actors.Count() > 0
						  from a in actors
						  where a.Actor_Movies != null && a.Actor_Movies.Count > 0
						  from am in a.Actor_Movies
						  where am.MovieId.Equals(m.Id)
						  select new { a, m }).ToList().DistinctBy(c => c.a.Id);

			return actorsMovies.Select(o => o.a).ToList();
		}

		/// <summary>
		/// Gets actor movies, by actor id
		/// </summary>
		/// <param name="actorId">actor id</param>
		/// <returns>Actor movies list</returns>
		public async Task<Actor> GetActorMoviesAsync(string actorId)
		{
			if (!await ActorExistsByIdAsync(actorId))
			{
				return null;
			}

			var actor = await _context.Actors
				.Where(a => a.Id.Equals(actorId))
				.Select(a => new Actor() { Id=a.Id, ProfilePicture=a.ProfilePicture, FullName=a.FullName, Bio=a.Bio, Actor_Movies=a.Actor_Movies })
				.FirstOrDefaultAsync();

			var actorMovies = (from m in await _context.Movies.ToListAsync()
						  where actor != null
						  from am in actor.Actor_Movies
						  where am.MovieId.Equals(m.Id)
						  select actor).FirstOrDefault();

			return actorMovies;
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

		public async Task<bool> CreateActor(Actor actor)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> Save()
		{
			return await _context.SaveChangesAsync() > 0 ? true : false;
		}
	}
}
