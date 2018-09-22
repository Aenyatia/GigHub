using AutoMapper;
using ConcertHub.Dtos;
using ConcertHub.Extensions;
using ConcertHub.Infrastructure.Data;
using ConcertHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConcertHub.Controllers.Api
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationsController : ControllerBase
	{
		private readonly ConcertContext _context;
		private readonly IMapper _mapper;

		public NotificationsController(ConcertContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetNewNotifications()
		{
			var userId = User.GetUserId();
			var notifications = _context.UserNotifications
				.Where(un => un.ArtistId == userId && !un.IsRead)
				.Select(un => un.Notification)
				.Include(n => n.Gig.Artist)
				.ToList();

			return Ok(_mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationDto>>(notifications));
		}
	}
}
