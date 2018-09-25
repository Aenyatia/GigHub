using AutoMapper;
using GigHub.Core.Domain;
using GigHub.Infrastructure.Extensions;
using GigHub.Infrastructure.Persistence.Data;
using GigHub.Web.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Controllers.Api
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationsController : ControllerBase
	{
		private readonly ApplicationContext _context;
		private readonly IMapper _mapper;

		public NotificationsController(ApplicationContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetNewNotifications()
		{
			var userId = User.GetUserId();
			var notifications = _context.UserNotifications
				.Where(un => un.UserId == userId && !un.IsRead)
				.Select(un => un.Notification)
				.Include(n => n.Gig.Artist)
				.ToList();

			return Ok(_mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationDto>>(notifications));
		}

		[HttpPost]
		public IActionResult MarkAsRed()
		{
			var userId = User.GetUserId();
			var notifications = _context.UserNotifications
				.Where(un => un.UserId == userId && !un.IsRead)
				.ToList();

			notifications.ForEach(n => n.Read());

			_context.SaveChanges();

			return Ok();
		}
	}
}
