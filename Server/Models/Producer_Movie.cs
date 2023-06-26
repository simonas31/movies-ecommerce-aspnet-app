using System.Text.Json.Serialization;

namespace Server.Models
{
	public class Producer_Movie
	{
		[JsonIgnore]
		public string MovieId { get; set; }
		public Movie Movie { get; set; }
		[JsonIgnore]
		public string ProducerId { get; set; }
		[JsonIgnore]
		public Producer Producer { get; set; }
	}
}
