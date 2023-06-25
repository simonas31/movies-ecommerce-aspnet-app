using Server.Models;

namespace Server.Interfaces
{
	public interface IProducersRepository
	{
		Task<ICollection<Producer>> GetAllAsync();
		Task<Producer> GetProducerAsync(string id);
		Task<ICollection<Producer>> GetByNameAsync(string name);
		Task<bool> ProducerExistsAsync(string name);
		Task<bool> ProducerExistsByIdAsync(string id);
	}
}
