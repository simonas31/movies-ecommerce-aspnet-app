using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.DTO;
using Server.Interfaces;
using Server.Models;
using System.Collections.Generic;
using System.Linq;

namespace Server.Repositories
{
	public class MoviesRepository : IMoviesRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly IActorsRepository _actorsRepository;
		private readonly ICinemasRepository _cinemasRepository;
		private readonly IProducersRepository _producersRepository;
		private readonly IMapper _mapper;

		public MoviesRepository(ApplicationDbContext context,
								IActorsRepository actorsRepository, 
								ICinemasRepository cinemasRepository, 
								IProducersRepository producersRepository,
								IMapper mapper)
		{
			_context = context;
			_actorsRepository = actorsRepository;
			_cinemasRepository = cinemasRepository;
			_producersRepository = producersRepository;
			_mapper = mapper;
		}

		/// <summary>
		/// Gets all movies ordered by start date
		/// </summary>
		/// <returns>movies list</returns>
		public async Task<ICollection<Movie>> GetAllAsync()
		{
			return await _context.Movies.OrderBy(m => m.StartDate).ToListAsync();
		}

		/// <summary>
		/// Gets movie by its id.
		/// </summary>
		/// <param name="id">movie id</param>
		/// <returns>Movie or null</returns>
		public async Task<Movie> GetMovieAsync(string id)
		{
			return await _context.Movies.Where(m => m.Id.Equals(id)).FirstOrDefaultAsync();
		}

		/// <summary>
		/// Gets movies by movie name
		/// </summary>
		/// <param name="name">movie name</param>
		/// <returns>filtered movies list by movie name</returns>
		public async Task<ICollection<Movie>> GetByNameAsync(string name)
		{
			return await _context.Movies.Where(m => m.Name.Contains(name)).ToListAsync();
		}

		/// <summary>
		/// Gets movies by movie category
		/// </summary>
		/// <param name="category">category name</param>
		/// <returns>filtered movies list by category name</returns>
		public async Task<ICollection<Movie>> GetByCategoryAsync(string category)
		{
			return await _context.Movies.Where(m => m.MovieCategory.Contains(category)).ToListAsync();
		}

		/// <summary>
		/// Gets movies of a cinema(-s), by cinema name
		/// </summary>
		/// <param name="cinemaName">cinema name</param>
		/// <returns>cinemas movies list</returns>
		public async Task<ICollection<CustomCinemaMovie>> GetCinemasMoviesAsync(string cinemaName)
		{
			if(!await _cinemasRepository.CinemaExistsAsync(cinemaName))
			{
				return null;
			}

			//get cinemas that contains cinemaName
			var cinemas = await _context.Cinemas.Where(c => c.Name.Contains(cinemaName)).ToListAsync();

			//get data from Cinemas_Movies table which contains found cinemas id
			var CinemaMoviesIds = (from cm in _context.Cinemas_Movies.ToList()
									   from c in cinemas
									   where cm.CinemaId.Equals(c.Id)
									   select new { cm.MovieId, cm.CinemaId }).ToList();

			//get movies that contains found cinema from movies table
			var movies = (from m in _context.Movies.ToList()
								  from cmi in CinemaMoviesIds
								  where cmi.MovieId.Equals(m.Id)
								  select new { cmi.CinemaId, m }).ToList();

			//Create custom output so it would be better visualy and easier to use
			List<CustomCinemaMovie> cinemaMovies = new List<CustomCinemaMovie>();
			for (int i = 0; i < movies.Count; i++)
			{
				if (!cinemaMovies.Exists(am => am.CinemaId.Equals(movies[i].CinemaId)))
				{
					var custom = new CustomCinemaMovie();
					custom.CinemaId = movies[i].CinemaId;
					custom.Movies.Add(_mapper.Map<MovieDTO>(movies[i].m));
					cinemaMovies.Add(custom);
				}
				else
					cinemaMovies.Find(am => am.CinemaId.Equals(movies[i].CinemaId)).Movies.Add(_mapper.Map<MovieDTO>(movies[i].m));
			}

			return cinemaMovies;
		}

		/// <summary>
		/// Gets movies of a producer(-s), by producer name
		/// </summary>
		/// <param name="producerName">producer name</param>
		/// <returns>producers movies list</returns>
		public async Task<ICollection<CustomProducerMovie>> GetProducersMoviesAsync(string producerName)
		{
			if (!await _producersRepository.ProducerExistsAsync(producerName))
			{
				return null;
			}
			//get producers that contains producerName
			var producers = await _context.Producers.Where(c => c.FullName.Contains(producerName)).ToListAsync();

			//get data from Producers_Movies table which contains found producers id
			var ProducerMoviesIds = (from pm in _context.Producers_Movies.ToList()
								   from p in producers
								   where pm.ProducerId.Equals(p.Id)
								   select new { pm.MovieId, pm.ProducerId }).ToList();

			//get movies that contains found producer from movies table
			var movies = (from m in _context.Movies.ToList()
						  from pmi in ProducerMoviesIds
						  where pmi.MovieId.Equals(m.Id)
						  select new { pmi.ProducerId, m }).ToList();

			//Create custom output so it would be better visualy and easier to use
			List<CustomProducerMovie> producersMovies = new List<CustomProducerMovie>();
			for (int i = 0; i < movies.Count; i++)
			{
				if (!producersMovies.Exists(am => am.ProducerId.Equals(movies[i].ProducerId)))
				{
					var custom = new CustomProducerMovie();
					custom.ProducerId = movies[i].ProducerId;
					custom.Movies.Add(_mapper.Map<MovieDTO>(movies[i].m));
					producersMovies.Add(custom);
				}
				else
					producersMovies.Find(am => am.ProducerId.Equals(movies[i].ProducerId)).Movies.Add(_mapper.Map<MovieDTO>(movies[i].m));
			}

			return producersMovies;
		}

		/// <summary>
		/// Gets movies of an actor(-s), by actor name
		/// </summary>
		/// <param name="actorName">actor name</param>
		/// <returns>actors movies list</returns>
		public async Task<ICollection<CustomActorMovie>> GetActorsMoviesAsync(string actorName)
		{
			if (!await _actorsRepository.ActorExistsAsync(actorName))
			{
				return null;
			}

			//get actors that contains actorName
			var actors = await _context.Actors.Where(c => c.FullName.Contains(actorName)).ToListAsync();

			//get data from Actors_Movies table which contains found actors id
			var actorMoviesIds = (from am in _context.Actors_Movies.ToList()
								   from a in actors
								   where am.ActorId.Equals(a.Id)
								   select new { am.MovieId, am.ActorId }).ToList();

			//get movies that contains found actor from movies table
			var movies = (from m in _context.Movies.ToList()
						  from ami in actorMoviesIds
						  where ami.MovieId.Equals(m.Id)
						  select new { ami.ActorId, m }).ToList();
			
			//Create custom output so it would be better visualy and easier to use
			List<CustomActorMovie> actorsMovies = new List<CustomActorMovie>();
			for(int i = 0; i < movies.Count; i++)
			{
				if(!actorsMovies.Exists(am => am.ActorId.Equals(movies[i].ActorId)))
				{
					var custom = new CustomActorMovie();
					custom.ActorId = movies[i].ActorId;
					custom.Movies.Add(_mapper.Map<MovieDTO>(movies[i].m));
					actorsMovies.Add(custom);
				}
				else
					actorsMovies.Find(am => am.ActorId.Equals(movies[i].ActorId)).Movies.Add(_mapper.Map<MovieDTO>(movies[i].m));
			}

			return actorsMovies;
		}

		/// <summary>
		/// Gets movies of a cinema, by cinema id
		/// </summary>
		/// <param name="cinemaId">cinema id</param>
		/// <returns>Cinema movies list</returns>
		public async Task<ICollection<Movie>> GetCinemaMoviesAsync(string cinemaId)
		{
			if (!await _cinemasRepository.CinemaExistsByIdAsync(cinemaId))
			{
				return null;
			}

			var cinemas = await _context.Cinemas.Where(c => c.Id.Equals(cinemaId)).FirstOrDefaultAsync();

			var cinemaMoviesIds = await _context.Cinemas_Movies.Where(cm => cm.CinemaId.Equals(cinemas.Id)).ToListAsync();

			var movies = (from m in await _context.Movies.ToListAsync()
						  from cmi in cinemaMoviesIds
						  where cmi.MovieId.Equals(m.Id)
						  select m).ToList();

			return movies;
		}

		/// <summary>
		/// Gets movies of a producer, by producer id
		/// </summary>
		/// <param name="producerId">producer id</param>
		/// <returns>Producer movies list</returns>
		public async Task<ICollection<Movie>> GetProducerMoviesAsync(string producerId)
		{
			if (!await _producersRepository.ProducerExistsByIdAsync(producerId))
			{
				return null;
			}

			var producer = await _context.Producers.Where(c => c.Id.Equals(producerId)).FirstOrDefaultAsync();

			var producerMoviesIds = await _context.Producers_Movies.Where(pc => pc.ProducerId.Equals(producer.Id)).ToListAsync();

			var movies = (from m in await _context.Movies.ToListAsync()
						  from pmi in producerMoviesIds
						  where pmi.MovieId.Equals(m.Id)
						  select m).ToList();

			return movies;
		}

		/// <summary>
		/// Gets movies of an actor, by actor id
		/// </summary>
		/// <param name="actorId">actor id</param>
		/// <returns>Actor movies list</returns>
		public async Task<ICollection<Movie>> GetActorMoviesAsync(string actorId)
		{
			if (!await _actorsRepository.ActorExistsByIdAsync(actorId))
			{
				return null;
			}

			var actor = await _context.Actors.Where(c => c.Id.Equals(actorId)).FirstOrDefaultAsync();

			var actorMoviesIds = await _context.Actors_Movies.Where(ac => ac.ActorId.Equals(actor.Id)).ToListAsync();

			var movies = (from m in await _context.Movies.ToListAsync()
						  from ami in actorMoviesIds
						  where ami.MovieId.Equals(m.Id)
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
		/// Find if movie exists with its id.
		/// </summary>
		/// <param name="id">movie id</param>
		/// <returns>true or false</returns>
		public async Task<bool> MovieExistsByIdAsync(string id)
		{
			return await _context.Movies.AnyAsync(m => m.Id.Equals(id));
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
