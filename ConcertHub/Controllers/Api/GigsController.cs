﻿using ConcertHub.Extensions;
using ConcertHub.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ConcertHub.Controllers.Api
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class GigsController : ControllerBase
	{
		private readonly ConcertContext _context;

		public GigsController(ConcertContext context)
		{
			_context = context;
		}

		[HttpDelete("{id}")]
		public IActionResult Cancel(int id)
		{
			var userId = User.GetUserId();
			var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

			if (gig.IsCanceled)
				return NotFound();

			gig.IsCanceled = true;
			_context.SaveChanges();

			return Ok();
		}
	}
}