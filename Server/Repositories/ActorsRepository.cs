using Server.Data;
using Server.Interfaces;
using Server.Models;

namespace Server.Repositories
{
	public class ActorsRepository : IActorsRepository
	{
		ApplicationDbContext _context;
		public ActorsRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public Task<IEnumerable<Actor>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Actor>> GetByNameAsync(string name)
		{
			throw new NotImplementedException();
		}
	}
}
