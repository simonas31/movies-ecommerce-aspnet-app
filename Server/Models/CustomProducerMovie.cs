using Server.DTO;

namespace Server.Models
{
	public class CustomProducerMovie
	{
		public string ProducerId { get; set; }
		public ICollection<MovieDTO> Movies { get; set; }

		public CustomProducerMovie()
		{
			this.Movies = new List<MovieDTO>();
		}
	}
}
