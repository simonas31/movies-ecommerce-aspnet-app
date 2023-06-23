using AutoMapper;
using Server.DTO;
using Server.Models;

namespace Server.Helper
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Actor, ActorDTO>();
			CreateMap<Cinema, CinemaDTO>();
			CreateMap<Movie, MovieDTO>();
			CreateMap<Producer, ProducerDTO>();
		}
	}
}
