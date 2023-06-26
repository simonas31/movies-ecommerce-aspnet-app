using System.Text.Json.Serialization;

namespace Server.Models
{
	public class Cinema_Movie
	{
		[JsonIgnore]
		public string MovieId { get; set; }
		public Movie Movie { get; set; }
		[JsonIgnore]
		public string CinemaId { get; set; }
		[JsonIgnore]
		public Cinema Cinema { get; set; }
	}
}
