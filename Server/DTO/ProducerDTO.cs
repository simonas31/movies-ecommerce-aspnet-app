using Server.Models;

namespace Server.DTO
{
	public class ProducerDTO
	{
		public string Id { get; set; }
		public byte[] ProfilePicture { get; set; }
		public string FullName { get; set; }
		public string Description { get; set; }
	}
}
