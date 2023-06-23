namespace Server.Models
{
	public class Actor_Movie
	{
		public string MovieId { get; set; }
		public Movie Movie { get; set; }
		public string ActorId { get; set; }
		public Actor Actor { get; set; }
	}
}
