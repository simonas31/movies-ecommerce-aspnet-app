using Server.Models;

namespace Server.Interfaces
{
	public interface IActorsRepository
	{
		Task<IEnumerable<Actor>> GetAllAsync();
		Task<IEnumerable<Actor>> GetByNameAsync(string name);
		Task<bool> ActorExistsAsync(string name);
	}
}
