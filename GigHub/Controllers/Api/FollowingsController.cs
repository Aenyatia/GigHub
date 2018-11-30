using GigHub.Core.Domain;
using GigHub.Infrastructure.Extensions;
using GigHub.Infrastructure.Persistence.Data;
using GigHub.Web.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GigHub.Controllers.Api
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class FollowingsController : ControllerBase
	{
		private readonly ApplicationDbContext _dbContext;

		public FollowingsController(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpPost]
		public IActionResult Follow([FromBody] FollowingDto dto)
		{
			var userId = User.GetUserId();

			if (_dbContext.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId))
				return BadRequest("The followee already exists.");

			var follow = new Following
			{
				FollowerId = userId,
				FolloweeId = dto.FolloweeId
			};

			_dbContext.Followings.Add(follow);
			_dbContext.SaveChanges();

			return Ok();
		}

		[HttpPost("{artistId}")]
		public IActionResult CancelFollowing(string artistId)
		{
			var userId = User.GetUserId();
			var follow = _dbContext.Followings.SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == artistId);

			if (follow == null)
				return NotFound();

			_dbContext.Followings.Remove(follow);
			_dbContext.SaveChanges();

			return Ok();
		}
	}
}
