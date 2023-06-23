using Server.Data;
using Server.Interfaces;
using Server.Models;

namespace Server.Repositories
{
	public class ProducersRepository : IProducersRepository
	{
		ApplicationDbContext _context;
		public ProducersRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public Task<IEnumerable<Producer>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Producer>> GetByNameAsync(string name)
		{
			throw new NotImplementedException();
		}
	}
}
