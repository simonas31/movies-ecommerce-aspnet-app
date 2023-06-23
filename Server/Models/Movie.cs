namespace Server.Models
{
	public class Movie
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public byte[] Image { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string MovieCategory { get; set; }

		//Relationships
		public ICollection<Actor_Movie> Actors_Movies { get; set; }
		public ICollection<Cinema_Movie> Cinemas_Movies { get; set; }
		public ICollection<Producer_Movie> Producers_Movies { get; set; }
	}
}
