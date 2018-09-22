using AutoMapper;
using ConcertHub.Dtos;
using ConcertHub.Models;

namespace ConcertHub.Mappers
{
	public static class AutoMapperMaps
	{
		public static IMapper Register()
			=> new MapperConfiguration(cfg =>
				{
					cfg.CreateMap<Artist, UserDto>();
					cfg.CreateMap<Gig, GigDto>();
					cfg.CreateMap<Notification, NotificationDto>();
					cfg.CreateMap<Genre, GigDto>();
				})
				.CreateMapper();
	}
}
