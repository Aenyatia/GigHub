using GigHub.Infrastructure.Extensions;
using GigHub.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GigHub.Controllers.Api
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class GigsController : ControllerBase
	{
		private readonly ApplicationDbContext _dbContext;

		public GigsController(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpDelete("{id}")]
		public IActionResult Cancel(int id)
		{
			var userId = User.GetUserId();
			var gig = _dbContext.Gigs
				.Include(g => g.Attendances)
				.ThenInclude(a => a.Attendee)
				.Single(g => g.Id == id && g.ArtistId == userId);

			if (gig.IsCanceled)
				return NotFound();

			gig.Cancel();

			_dbContext.SaveChanges();

			return Ok();
		}
	}
}
