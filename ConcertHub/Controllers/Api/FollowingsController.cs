using ConcertHub.Dtos;
using ConcertHub.Extensions;
using ConcertHub.Infrastructure.Data;
using ConcertHub.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ConcertHub.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class FollowingsController : ControllerBase
	{
		private readonly ConcertContext _context;

		public FollowingsController(ConcertContext context)
		{
			_context = context;
		}

		[HttpPost]
		public IActionResult Follow([FromBody] FollowingDto dto)
		{
			var userId = User.GetUserId();

			if (_context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId))
				return BadRequest("The followee already exists.");

			var follow = new Following
			{
				FollowerId = userId,
				FolloweeId = dto.FolloweeId
			};

			_context.Followings.Add(follow);
			_context.SaveChanges();

			return Ok();
		}
	}
}
