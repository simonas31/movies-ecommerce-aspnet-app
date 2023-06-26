using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.DTO;
using Server.Interfaces;
using Server.Models;
using System.Collections.Generic;
using System.Linq;

namespace Server.Repositories
{
	public class MoviesRepository : IMoviesRepository
	{
		private readonly ApplicationDbContext _context;

		public MoviesRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Gets all movies ordered by start date
		/// </summary>
		/// <returns>movies list</returns>
		public async Task<ICollection<Movie>> GetAllAsync()
		{
			return await _context.Movies.OrderBy(m => m.StartDate).ToListAsync();
		}

		/// <summary>
		/// Gets movie by its id.
		/// </summary>
		/// <param name="id">movie id</param>
		/// <returns>Movie or null</returns>
		public async Task<Movie> GetMovieAsync(string id)
		{
			return await _context.Movies.Where(m => m.Id.Equals(id)).FirstOrDefaultAsync();
		}

		/// <summary>
		/// Gets movies by movie name
		/// </summary>
		/// <param name="name">movie name</param>
		/// <returns>filtered movies list by movie name</returns>
		public async Task<ICollection<Movie>> GetByNameAsync(string name)
		{
			return await _context.Movies.Where(m => m.Name.Contains(name)).ToListAsync();
		}

		/// <summary>
		/// Gets movies by movie category
		/// </summary>
		/// <param name="category">category name</param>
		/// <returns>filtered movies list by category name</returns>
		public async Task<ICollection<Movie>> GetByCategoryAsync(string category)
		{
			return await _context.Movies.Where(m => m.MovieCategory.Contains(category)).ToListAsync();
		}

		/// <summary>
		/// Finds if movie exists
		/// </summary>
		/// <param name="name">name of the movie</param>
		/// <returns>true or false</returns>
		public async Task<bool> MovieExistsAsync(string name)
		{
			return await _context.Movies.AnyAsync(m => m.Name.Contains(name));
		}

		/// <summary>
		/// Find if movie exists with its id.
		/// </summary>
		/// <param name="id">movie id</param>
		/// <returns>true or false</returns>
		public async Task<bool> MovieExistsByIdAsync(string id)
		{
			return await _context.Movies.AnyAsync(m => m.Id.Equals(id));
		}

		/// <summary>
		/// Finds if movie exists that has a specific category
		/// </summary>
		/// <param name="category">category name</param>
		/// <returns>true or false</returns>
		public async Task<bool> MovieExistsByCategoryAsync(string category)
		{
			return await _context.Movies.AnyAsync(m => m.MovieCategory.Contains(category));
		}
	}
}
