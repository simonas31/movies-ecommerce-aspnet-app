using Server.Models;

namespace Server.Interfaces
{
	public interface ICinemasRepository
	{
		Task<IEnumerable<Cinema>> GetAllAsync();
		Task<IEnumerable<Cinema>> GetByNameAsync(string name);
	}
}
