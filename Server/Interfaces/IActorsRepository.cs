using Server.Models;

namespace Server.Interfaces
{
	public interface IActorsRepository
	{
		Task<ICollection<Actor>> GetAllAsync();
		Task<Actor> GetActorAsync(string id);
		Task<ICollection<Actor>> GetByNameAsync(string name);
		Task<ICollection<Actor>> GetActorsMoviesAsync(string actorName);
		Task<Actor> GetActorMoviesAsync(string actorId);
		Task<bool> ActorExistsAsync(string name);
		Task<bool> ActorExistsByIdAsync(string id);
		Task<bool> CreateActor(Actor actor);
		Task<bool> Save();
	}
}
