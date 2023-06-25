namespace Server.DTO
{
	public class MovieDTO
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public byte[] Image { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string MovieCategory { get; set; }
	}
}
