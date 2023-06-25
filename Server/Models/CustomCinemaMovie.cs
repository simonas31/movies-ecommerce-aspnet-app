using Server.DTO;

namespace Server.Models
{
	public class CustomCinemaMovie
	{
		public string CinemaId { get; set; }
		public ICollection<MovieDTO> Movies { get; set; }

		public CustomCinemaMovie()
		{
			this.Movies = new List<MovieDTO>();
		}
	}
}
