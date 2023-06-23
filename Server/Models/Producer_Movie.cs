namespace Server.Models
{
	public class Producer_Movie
	{
		public string MovieId { get; set; }
		public Movie Movie { get; set; }
		public string ProducerId { get; set; }
		public Producer Producer { get; set; }
	}
}
