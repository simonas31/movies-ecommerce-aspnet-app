using Server.Models;

namespace Server.Interfaces
{
	public interface IMoviesRepository
	{
		Task<ICollection<Movie>> GetAllAsync();
		Task<Movie> GetMovieAsync(string id);
		Task<ICollection<Movie>> GetByNameAsync(string name);
		Task<ICollection<Movie>> GetByCategoryAsync(string category);
		Task<ICollection<CustomCinemaMovie>> GetCinemasMoviesAsync(string cinemaName);
		Task<ICollection<CustomProducerMovie>> GetProducersMoviesAsync(string producerName);
		Task<ICollection<CustomActorMovie>> GetActorsMoviesAsync(string actorName);
		Task<ICollection<Movie>> GetCinemaMoviesAsync(string cinemaId);
		Task<ICollection<Movie>> GetProducerMoviesAsync(string producerId);
		Task<ICollection<Movie>> GetActorMoviesAsync(string actorId);
		Task<bool> MovieExistsAsync(string name);
		Task<bool> MovieExistsByIdAsync(string id);
		Task<bool> MovieExistsByCategoryAsync(string category);
	}
}
