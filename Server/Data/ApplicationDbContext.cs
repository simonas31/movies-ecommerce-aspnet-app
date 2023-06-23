using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			//actor_movie join table foreign keys
			builder.Entity<Actor_Movie>()
				.HasKey(am => new { am.MovieId, am.ActorId });
			builder.Entity<Actor_Movie>()
				.HasOne(m => m.Movie)
				.WithMany(am => am.Actors_Movies)
				.HasForeignKey(m => m.MovieId);
			builder.Entity<Actor_Movie>()
				.HasOne(a => a.Actor)
				.WithMany(am => am.Actors_Movies)
				.HasForeignKey(a => a.ActorId);

			//cinema_movie join table foreign keys
			builder.Entity<Cinema_Movie>()
				.HasKey(cm => new { cm.MovieId, cm.CinemaId });
			builder.Entity<Cinema_Movie>()
				.HasOne(m => m.Movie)
				.WithMany(cm => cm.Cinemas_Movies)
				.HasForeignKey(m => m.MovieId);
			builder.Entity<Cinema_Movie>()
				.HasOne(c => c.Cinema)
				.WithMany(cm => cm.Cinemas_Movies)
				.HasForeignKey(c => c.CinemaId);

			//producer_movie join table foreign keys
			builder.Entity<Producer_Movie>()
				.HasKey(cm => new { cm.MovieId, cm.ProducerId });
			builder.Entity<Producer_Movie>()
				.HasOne(m => m.Movie)
				.WithMany(pm => pm.Producers_Movies)
				.HasForeignKey(m => m.MovieId);
			builder.Entity<Producer_Movie>()
				.HasOne(p => p.Producer)
				.WithMany(pm => pm.Producers_Movies)
				.HasForeignKey(p => p.ProducerId);
		}

		public DbSet<Producer_Movie> Producers_Movies { get; set; }
		public DbSet<Cinema_Movie> Cinemas_Movies { get; set; }
		public DbSet<Actor_Movie> Actors_Movies { get; set; }
		public DbSet<Actor> Actors { get; set; }
		public DbSet<Cinema> Cinemas { get; set; }
		public DbSet<Movie> Movies { get; set; }
		public DbSet<Producer> Producers { get; set; }
	}
}
