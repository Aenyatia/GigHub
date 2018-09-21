using ConcertHub.Infrastructure.Data;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ConcertHub.Controllers
{
	public class HomeController : Controller
	{
		private readonly ConcertContext _concertContext;

		public HomeController(ConcertContext concertContext)
		{
			_concertContext = concertContext;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var upcomingGigs = _concertContext.Gigs
				.Include(g => g.Artist)
				.Include(g => g.Genre)
				.Where(g => g.DateTime > DateTime.UtcNow)
				.ToList();

			var viewModel = new GigsViewModel
			{
				UpcomingGigs = upcomingGigs,
				ShowActions = User.Identity.IsAuthenticated,
				Heading = "Upcoming gigs"
			};

			return View("Gigs", viewModel);
		}
	}
}
