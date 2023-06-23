namespace Server.Models
{
	public class Cinema
	{
		public string Id { get; set; }
		public byte[] Logo { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		//Relationships
		public ICollection<Cinema_Movie> Cinemas_Movies { get; set; }
	}
}
