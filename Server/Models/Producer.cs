namespace Server.Models
{
	public class Producer
	{
		public string Id { get; set; }
		public byte[] ProfilePicture { get; set; }
		public string FullName { get; set; }
		public string Description { get; set; }

		//Relationships
		public ICollection<Producer_Movie> Producer_Movies { get; set; }
	}
}
