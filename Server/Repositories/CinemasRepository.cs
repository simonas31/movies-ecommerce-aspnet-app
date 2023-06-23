using Server.Data;
using Server.Interfaces;
using Server.Models;

namespace Server.Repositories
{
	public class CinemasRepository : ICinemasRepository
	{
		ApplicationDbContext _context;
		public CinemasRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public Task<IEnumerable<Cinema>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Cinema>> GetByNameAsync(string name)
		{
			throw new NotImplementedException();
		}
	}
}
