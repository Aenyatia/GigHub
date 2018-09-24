using ConcertHub.Extensions;
using ConcertHub.Infrastructure.Data;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using ConcertHub.Repositories;

namespace ConcertHub.Controllers
{
	public class HomeController : Controller
	{
		private readonly ConcertContext _context;
		private readonly AttendanceRepository _attendanceRepository;

		public HomeController(ConcertContext context)
		{
			_context = context;
			_attendanceRepository = new AttendanceRepository(_context);
		}

		[HttpGet]
		public IActionResult Index(string query = null)
		{
			var upcomingGigs = _context.Gigs
				.Include(g => g.Artist)
				.Include(g => g.Genre)
				.Where(g => g.DateTime > DateTime.UtcNow && !g.IsCanceled);

			if (!string.IsNullOrWhiteSpace(query))
			{
				upcomingGigs = upcomingGigs.Where(g => g.Artist.Name.Contains(query) ||
													   g.Genre.Name.Contains(query) ||
													   g.Venue.Contains(query));
			}

			var userId = User.GetUserId();
			var attendances = _attendanceRepository.GetFutureAttendances(userId)
				.ToLookup(a => a.GigId);

			var viewModel = new GigsViewModel
			{
				UpcomingGigs = upcomingGigs,
				ShowActions = User.Identity.IsAuthenticated,
				Heading = "Upcoming gigs",
				SearchTerm = query,
				Attendances = attendances
			};

			return View("Gigs", viewModel);
		}
	}
}
