using ConcertHub.Extensions;
using ConcertHub.Infrastructure.Data;
using ConcertHub.Infrastructure.Identity;
using ConcertHub.Models;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ConcertHub.Controllers
{
	[Authorize]
	public class GigsController : Controller
	{
		private readonly ConcertContext _context;
		private readonly IdentityContext _identityContext;

		public GigsController(ConcertContext context, IdentityContext identityContext)
		{
			_context = context;
			_identityContext = identityContext;
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
			var artist = _identityContext.Users.Single(u => u.Id == GetCurrentUserId());
			var genre = _context.Genres.Single(g => g.Id == viewModel.GenreId);

			var gig = new Gig
			{
				Artist = artist,
				DateTime = DateTime.Parse($"{viewModel.Date} {viewModel.Time}"),
				Genre = genre,
				Venue = viewModel.Venue
			};

			_context.Gigs.Add(gig);
			_context.SaveChanges();

			return RedirectToAction("Index", "Home");
		}

		private string GetCurrentUserId()
			=> User.GetUserId();
	}
}
