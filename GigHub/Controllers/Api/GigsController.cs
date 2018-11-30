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
		private readonly ApplicationContext _context;

		public GigsController(ApplicationContext context)
		{
			_context = context;
		}

		[HttpDelete("{id}")]
		public IActionResult Cancel(int id)
		{
			var userId = User.GetUserId();
			var gig = _context.Gigs
				.Include(g => g.Attendances)
				.ThenInclude(a => a.Attendee)
				.Single(g => g.Id == id && g.ArtistId == userId);

			if (gig.IsCanceled)
				return NotFound();

			gig.Cancel();

			_context.SaveChanges();

			return Ok();
		}
	}
}
