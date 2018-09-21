using ConcertHub.Infrastructure.Data;
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
				.Where(g => g.DateTime > DateTime.UtcNow)
				.ToList();

			return View(upcomingGigs);
		}
	}
}
