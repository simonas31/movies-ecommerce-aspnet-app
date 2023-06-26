namespace Server.Models
{
	public class Cinema
	{
		public string Id { get; set; }
		public byte[] Logo { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		//Relationships
		public ICollection<Cinema_Movie> Cinema_Movies { get; set; }
	}
}
