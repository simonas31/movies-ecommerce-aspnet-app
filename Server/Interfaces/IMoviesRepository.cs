using Server.Models;

namespace Server.Interfaces
{
	public interface IMoviesRepository
	{
		Task<ICollection<Movie>> GetAllAsync();
		Task<Movie> GetMovieAsync(string id);
		Task<ICollection<Movie>> GetByNameAsync(string name);
		Task<ICollection<Movie>> GetByCategoryAsync(string category);
		Task<bool> MovieExistsAsync(string name);
		Task<bool> MovieExistsByIdAsync(string id);
		Task<bool> MovieExistsByCategoryAsync(string category);
	}
}
