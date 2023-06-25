using Server.DTO;

namespace Server.Models
{
	/// <summary>
	/// Class used for GetActorsMoviesAsync method in MovieController.
	/// </summary>
	public class CustomActorMovie
	{
		public string ActorId { get; set; }
		public ICollection<MovieDTO> Movies { get; set; }

		public CustomActorMovie()
		{
			this.Movies = new List<MovieDTO>();
		}
	}
}
