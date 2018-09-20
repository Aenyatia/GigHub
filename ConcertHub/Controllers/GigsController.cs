using ConcertHub.Extensions;
using ConcertHub.Infrastructure.Data;
using ConcertHub.Models;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ConcertHub.Controllers
{
	[Authorize]
	public class GigsController : Controller
	{
		private readonly ConcertContext _context;

		public GigsController(ConcertContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Create()
		{
			var viewModel = new GigFormViewModel { Genres = _context.Genres.ToList() };

			return View(viewModel);
		}

		[HttpPost]
		public IActionResult Create(GigFormViewModel viewModel)
		{
			var gig = new Gig
			{
				ArtistId = User.GetUserId(),
				DateTime = viewModel.DateTime,
				GenreId = viewModel.GenreId,
				Venue = viewModel.Venue
			};

			_context.Gigs.Add(gig);
			_context.SaveChanges();

			return RedirectToAction("Index", "Home");
		}
	}
}
