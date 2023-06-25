using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Interfaces;
using Server.Models;
using System.Linq;

namespace Server.Repositories
{
	public class MoviesRepository : IMoviesRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly IActorsRepository _actorsRepository;
		private readonly ICinemasRepository _cinemasRepository;
		private readonly IProducersRepository _producersRepository;
		public MoviesRepository(ApplicationDbContext context,
								IActorsRepository actorsRepository, 
								ICinemasRepository cinemasRepository, 
								IProducersRepository producersRepository)
		{
			_context = context;
			_actorsRepository = actorsRepository;
			_cinemasRepository = cinemasRepository;
			_producersRepository = producersRepository;
		}

		/// <summary>
		/// Gets all movies ordered by start date
		/// </summary>
		/// <returns>movies list</returns>
		public async Task<IEnumerable<Movie>> GetAllAsync()
		{
			return await _context.Movies.OrderBy(m => m.StartDate).ToListAsync();
		}

		/// <summary>
		/// Gets movies by movie name
		/// </summary>
		/// <param name="name">movie name</param>
		/// <returns>filtered movies list by movie name</returns>
		public async Task<IEnumerable<Movie>> GetByNameAsync(string name)
		{
			return await _context.Movies.Where(m => m.Name.Contains(name)).ToListAsync();
		}

		/// <summary>
		/// Gets movies by movie category
		/// </summary>
		/// <param name="category">category name</param>
		/// <returns>filtered movies list by category name</returns>
		public async Task<IEnumerable<Movie>> GetByCategoryAsync(string category)
		{
			return await _context.Movies.Where(m => m.MovieCategory.Contains(category)).ToListAsync();
		}

		/// <summary>
		/// Gets movies of a specific cinema, by cinema name
		/// </summary>
		/// <param name="cinemaName">cinema name</param>
		/// <returns>Specified cinema movies list</returns>
		public async Task<IEnumerable<Movie>> GetCinemaMoviesAsync(string cinemaName)
		{
			if(!await _cinemasRepository.CinemaExistsAsync(cinemaName))
			{
				return null;
			}

			var cinemas = await _context.Cinemas.Where(c => c.Name.Equals(cinemaName)).ToListAsync();

			var CinemaMoviesIds = (from cm in _context.Cinemas_Movies.ToList()
									   from c in cinemas
									   where cm.CinemaId == c.Id
									   select cm.MovieId).ToList();

			var movies = (from m in _context.Movies.ToList()
								  from cmi in CinemaMoviesIds
								  where cmi.Equals(m.Id)
								  select m).ToList();

			return movies;
		}

		/// <summary>
		/// Gets movies of a producer, by producer name
		/// </summary>
		/// <param name="producerName">producer name</param>
		/// <returns>Specific producer movies list</returns>
		public async Task<IEnumerable<Movie>> GetProducerMoviesAsync(string producerName)
		{
			if (!await _producersRepository.ProducerExistsAsync(producerName))
			{
				return null;
			}

			var producers = await _context.Producers.Where(c => c.FullName.Equals(producerName)).ToListAsync();

			var ProducerMoviesIds = (from pm in _context.Producers_Movies.ToList()
								   from p in producers
								   where pm.ProducerId == p.Id
								   select pm.MovieId).ToList();

			var movies = (from m in _context.Movies.ToList()
						  from pmi in ProducerMoviesIds
						  where pmi.Equals(m.Id)
						  select m).ToList();

			return movies;
		}

		/// <summary>
		/// Gets movies of an actor, by actor name
		/// </summary>
		/// <param name="actorName">actor name</param>
		/// <returns>Specific actor movies list</returns>
		public async Task<IEnumerable<Movie>> GetActorMoviesAsync(string actorName)
		{
			if (!await _actorsRepository.ActorExistsAsync(actorName))
			{
				return null;
			}

			var actors = await _context.Actors.Where(c => c.FullName.Equals(actorName)).ToListAsync();

			var CinemaMoviesIds = (from am in _context.Actors_Movies.ToList()
								   from a in actors
								   where am.ActorId == a.Id
								   select am.MovieId).ToList();

			var movies = (from m in _context.Movies.ToList()
						  from cmi in CinemaMoviesIds
						  where cmi.Equals(m.Id)
						  select m).ToList();

			return movies;
		}

		/// <summary>
		/// Finds if movie exists
		/// </summary>
		/// <param name="name">name of the movie</param>
		/// <returns>true or false</returns>
		public async Task<bool> MovieExistsAsync(string name)
		{
			return await _context.Movies.AnyAsync(m => m.Name.Contains(name));
		}

		/// <summary>
		/// Finds if movie exists that has a specific category
		/// </summary>
		/// <param name="category">category name</param>
		/// <returns>true or false</returns>
		public async Task<bool> MovieExistsByCategoryAsync(string category)
		{
			return await _context.Movies.AnyAsync(m => m.MovieCategory.Contains(category));
		}
	}
}
