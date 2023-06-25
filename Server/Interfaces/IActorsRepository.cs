using Server.Models;

namespace Server.Interfaces
{
	public interface IActorsRepository
	{
		Task<ICollection<Actor>> GetAllAsync();
		Task<Actor> GetActorAsync(string id);
		Task<ICollection<Actor>> GetByNameAsync(string name);
		Task<bool> ActorExistsAsync(string name);
		Task<bool> ActorExistsByIdAsync(string id);
	}
}
