using ConcertHub.Dtos;
using ConcertHub.Extensions;
using ConcertHub.Infrastructure.Data;
using ConcertHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ConcertHub.Controllers.Api
{
	[Authorize]
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

		[HttpPost("{artistId}")]
		public IActionResult CancelFollowing(string artistId)
		{
			var userId = User.GetUserId();
			var follow = _context.Followings.SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == artistId);

			if (follow == null)
				return NotFound();

			_context.Followings.Remove(follow);
			_context.SaveChanges();

			return Ok();
		}
	}
}
