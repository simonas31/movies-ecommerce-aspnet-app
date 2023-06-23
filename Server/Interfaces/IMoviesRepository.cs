using Server.Models;

namespace Server.Interfaces
{
	public interface IMoviesRepository
	{
		Task<IEnumerable<Movie>> GetAllAsync();
		Task<IEnumerable<Movie>> GetByNameAsync(string name);
		Task<IEnumerable<Movie>> GetByCategoryAsync(string category);
		Task<IEnumerable<Movie>> GetCinemaMoviesAsync(string cinemaName);
		Task<IEnumerable<Movie>> GetProducerMoviesAsync(string producerName);
		Task<IEnumerable<Movie>> GetActorMoviesAsync(string actorName);
		Task<bool> MovieExistsAsync(string name);
	}
}
