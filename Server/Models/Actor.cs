namespace Server.Models
{
	public class Actor
	{
		public string Id { get; set; }
		public byte[] ProfilePicture { get; set; }
		public string FullName { get; set; }
		public string Bio { get; set; }

		//Relationships
		public ICollection<Actor_Movie> Actors_Movies { get; set; }
	}
}
