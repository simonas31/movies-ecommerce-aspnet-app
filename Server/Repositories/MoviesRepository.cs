using Server.Data;
using Server.Interfaces;
using Server.Models;

namespace Server.Repositories
{
	public class MoviesRepository : IMoviesRepository
	{
		ApplicationDbContext _context;
		public MoviesRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public Task<IEnumerable<Movie>> GetActorMoviesAsync(string actorName)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Movie>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Movie>> GetByCategoryAsync(string category)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Movie>> GetByNameAsync(string name)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Movie>> GetCinemaMoviesAsync(string cinemaName)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Movie>> GetProducerMoviesAsync(string producerName)
		{
			throw new NotImplementedException();
		}
	}
}
