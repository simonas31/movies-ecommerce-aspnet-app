namespace Server.Models
{
	public class Cinema_Movie
	{
		public string MovieId { get; set; }
		public Movie Movie { get; set; }
		public string CinemaId { get; set; }
		public Cinema Cinema { get; set; }
	}
}
