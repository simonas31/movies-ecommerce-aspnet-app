using Server.Models;

namespace Server.Interfaces
{
	public interface IProducersRepository
	{
		Task<ICollection<Producer>> GetAllAsync();
		Task<Producer> GetProducerAsync(string id);
		Task<ICollection<Producer>> GetByNameAsync(string name);
		Task<ICollection<Producer>> GetProducersMoviesAsync(string producerName);
		Task<Producer> GetProducerMoviesAsync(string producerId);
		Task<bool> ProducerExistsAsync(string name);
		Task<bool> ProducerExistsByIdAsync(string id);
	}
}
