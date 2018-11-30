using GigHub.Core.Extensions;
using GigHub.Infrastructure.Extensions;
using GigHub.Infrastructure.Persistence.Data;
using GigHub.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace GigHub.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext _dbContext;

		public HomeController(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Index(string query = null)
		{
			var upcomingGigs = _dbContext.Gigs
				.Include(g => g.Artist)
				.Include(g => g.Genre)
				.Where(g => g.DateTime > DateTime.UtcNow && !g.IsCanceled);

			if (!query.IsEmpty())
			{
				upcomingGigs = upcomingGigs.Where(g => g.Artist.Name.Contains(query) ||
													   g.Genre.Name.Contains(query) ||
													   g.Venue.Contains(query));
			}

			var userId = User.GetUserId();
			var userAttendance = _dbContext.Attendances
					.Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.UtcNow)
					.ToLookup(a => a.GigId);

			var viewModel = new GigsViewModel
			{
				UpcomingGigs = upcomingGigs.ToList(),
				UserAttendance = userAttendance,
				SearchTerm = query,

				ShowActions = User.Identity.IsAuthenticated,
				Heading = "Upcoming gigs "
			};

			return View("Gigs", viewModel);
		}
	}
}
