using ConcertHub.Infrastructure.Data;
using ConcertHub.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ConcertHub.Controllers
{
	public class HomeController : Controller
	{
		private readonly ConcertContext _concertContext;
		private readonly UserManager<ApplicationUser> _userManager;

		public HomeController(ConcertContext concertContext, UserManager<ApplicationUser> userManager)
		{
			_concertContext = concertContext;
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var upcomingGigs = _concertContext.Gigs
				.Include(g => g.Artist)
				.Where(g => g.DateTime > DateTime.UtcNow)
				.ToList();

			return View(upcomingGigs);
		}
	}
}
