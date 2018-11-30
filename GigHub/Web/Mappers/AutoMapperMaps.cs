using AutoMapper;
using GigHub.Core.Domain;
using GigHub.Web.Dtos;

namespace GigHub.Web.Mappers
{
	public static class AutoMapperMaps
	{
		public static IMapper Register()
			=> new MapperConfiguration(cfg =>
				{
					cfg.CreateMap<User, UserDto>();
					cfg.CreateMap<Gig, GigDto>();
					cfg.CreateMap<Notification, NotificationDto>();
					cfg.CreateMap<Genre, GigDto>();
				})
				.CreateMapper();
	}
}
