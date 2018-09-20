using ConcertHub.Extensions;
using ConcertHub.Infrastructure.Data;
using ConcertHub.Infrastructure.Identity;
using ConcertHub.Models;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ConcertHub.Controllers
{
	[Authorize]
	public class GigsController : Controller
	{
		private readonly ConcertContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public GigsController(ConcertContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
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
			var artist = _userManager.Users.Single(u => u.Id == GetUserId());

			var gig = new Gig
			{
				ArtistId = artist.Id,
				DateTime = DateTime.Parse($"{viewModel.Date} {viewModel.Time}"),
				GenreId = viewModel.GenreId,
				Venue = viewModel.Venue
			};

			_context.Gigs.Add(gig);
			_context.SaveChanges();

			return RedirectToAction("Index", "Home");
		}

		private string GetUserId()
			=> User.GetUserId();
	}
}
