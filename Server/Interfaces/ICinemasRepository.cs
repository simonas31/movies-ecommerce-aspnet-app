using Server.Models;

namespace Server.Interfaces
{
	public interface ICinemasRepository
	{
		Task<ICollection<Cinema>> GetAllAsync();
		Task<Cinema> GetCinemaAsync(string id);
		Task<ICollection<Cinema>> GetByNameAsync(string name);
		Task<ICollection<Cinema>> GetCinemasMoviesAsync(string cinemaName);
		Task<Cinema> GetCinemaMoviesAsync(string cinemaId);
		Task<bool> CinemaExistsAsync(string name);
		Task<bool> CinemaExistsByIdAsync(string id);
	}
}
