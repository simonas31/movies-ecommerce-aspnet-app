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
			//CreateMap<Actor_Movie, ActorMovieDTO>();
			//CreateMap<Cinema_Movie, CinemaMovieDTO>();
			//CreateMap<Producer_Movie, ProducerMovieDTO>();
		}
	}
}
