using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Interfaces;
using Server.Models;

namespace Server.Repositories
{
	public class CinemasRepository : ICinemasRepository
	{
		private readonly ApplicationDbContext _context;
		public CinemasRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Gets all cinemas
		/// </summary>
		/// <returns>Cinemas list</returns>
		public async Task<ICollection<Cinema>> GetAllAsync()
		{
			return await _context.Cinemas.OrderBy(c => c.Name).ToListAsync();
		}

		/// <summary>
		/// Gets cinema by id
		/// </summary>
		/// <param name="id">cinema id</param>
		/// <returns>cinema or null</returns>
		public async Task<Cinema> GetCinemaAsync(string id)
		{
			return await _context.Cinemas.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
		}

		/// <summary>
		/// Gets cinemas by name
		/// </summary>
		/// <param name="name">cinema name</param>
		/// <returns>cinemas list</returns>
		public async Task<ICollection<Cinema>> GetByNameAsync(string name)
		{
			return await _context.Cinemas.Where(c => c.Name.Contains(name)).ToListAsync();
		}

		/// <summary>
		/// Gets cinema(-s) movies, by cinema name
		/// </summary>
		/// <param name="cinemaName">cinema name</param>
		/// <returns>cinemas movies list</returns>
		public async Task<ICollection<Cinema>> GetCinemasMoviesAsync(string cinemaName)
		{
			if (!await CinemaExistsAsync(cinemaName))
			{
				return null;
			}

			//get cinemas that contains cinema name
			var cinemas = await _context.Cinemas
				.Where(c => c.Name.Contains(cinemaName))
				.Select(c => new Cinema() { Id = c.Id, Logo = c.Logo, Name = c.Name, Description = c.Description, Cinema_Movies = c.Cinema_Movies })
				.ToListAsync();

			//get movies that contains found cinema from movies table
			var cinemasMovies = (from m in _context.Movies.ToList()
						  where cinemas.Count > 0
						  from c in cinemas
						  where c.Cinema_Movies != null & c.Cinema_Movies.Count() > 0
						  from cm in c.Cinema_Movies
						  where cm.MovieId.Equals(m.Id)
						  select new { c, m }).ToList().DistinctBy(c => c.c.Id);

			return cinemasMovies.Select(o => o.c).ToList();
		}

		/// <summary>
		/// Gets cinema movies, by cinema id
		/// </summary>
		/// <param name="cinemaId">cinema id</param>
		/// <returns>Cinema movies list</returns>
		public async Task<Cinema> GetCinemaMoviesAsync(string cinemaId)
		{
			if (!await CinemaExistsByIdAsync(cinemaId))
			{
				return null;
			}

			var cinema = await _context.Cinemas
				.Where(c => c.Id.Equals(cinemaId))
				.Select(c => new Cinema{ Id=c.Id, Logo=c.Logo, Name=c.Name, Description=c.Description, Cinema_Movies=c.Cinema_Movies })
				.FirstOrDefaultAsync();

			var cinemaMovies = (from m in await _context.Movies.ToListAsync()
						  where cinema != null
						  from cm in cinema.Cinema_Movies
						  where cm.MovieId.Equals(m.Id)
						  select cinema).FirstOrDefault();

			return cinemaMovies;
		}

		/// <summary>
		/// Checks if cinema exsist by cinema name
		/// </summary>
		/// <param name="name">cinema name</param>
		/// <returns>true or false</returns>
		public async Task<bool> CinemaExistsAsync(string name)
		{
			return await _context.Cinemas.AnyAsync(c => c.Name.Contains(name));
		}

		/// <summary>
		/// Checks if cinema exsist by cinema id
		/// </summary>
		/// <param name="id">cinema id</param>
		/// <returns>true or false</returns>
		public async Task<bool> CinemaExistsByIdAsync(string id)
		{
			return await _context.Cinemas.AnyAsync(c => c.Id.Equals(id));
		}
	}
}
