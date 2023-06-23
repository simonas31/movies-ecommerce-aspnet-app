using Server.Models;

namespace Server.Interfaces
{
	public interface IProducersRepository
	{
		Task<IEnumerable<Producer>> GetAllAsync();
		Task<IEnumerable<Producer>> GetByNameAsync(string name);

	}
}
