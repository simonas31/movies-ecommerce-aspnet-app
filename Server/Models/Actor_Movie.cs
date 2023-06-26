using System.Text.Json.Serialization;

namespace Server.Models
{
	public class Actor_Movie
	{
		[JsonIgnore]
		public string MovieId { get; set; }
		public Movie Movie { get; set; }
		[JsonIgnore]
		public string ActorId { get; set; }
		[JsonIgnore]
		public Actor Actor { get; set; }
	}
}
