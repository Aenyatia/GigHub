using ConcertHub.Extensions;
using ConcertHub.Infrastructure.Data;
using ConcertHub.Models;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
			var viewModel = new GigFormViewModel
			{
				Genres = _context.Genres.ToList(),
				Heading = "Add a Gig"
			};

			return View("GigForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(GigFormViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel.Genres = _context.Genres.ToList();
				return View("GigForm", viewModel);
			}

			var gig = new Gig
			{
				ArtistId = User.GetUserId(),
				DateTime = viewModel.GetDateTime(),
				GenreId = viewModel.GenreId,
				Venue = viewModel.Venue
			};

			_context.Gigs.Add(gig);
			_context.SaveChanges();

			return RedirectToAction("Mine", "Gigs");
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			var userId = User.GetUserId();
			var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

			var viewModel = new GigFormViewModel
			{
				Genres = _context.Genres.ToList(),
				Date = gig.DateTime.ToString("yyyy-MM-dd"),
				Time = gig.DateTime.ToString("HH:mm"),
				GenreId = gig.GenreId,
				Venue = gig.Venue,
				Heading = "Edit a Gig"
			};

			return View("GigForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(GigFormViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel.Genres = _context.Genres.ToList();
				return View("GigForm", viewModel);
			}

			var userId = User.GetUserId();
			var gig = _context.Gigs
				.Include(g => g.Attendances)
				.ThenInclude(a => a.Attendee)
				.Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

			gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.GenreId);

			_context.SaveChanges();

			return RedirectToAction("Mine", "Gigs");
		}

		[HttpGet]
		public IActionResult Mine()
		{
			var userId = User.GetUserId();
			var gigs = _context.Gigs
				.Where(g => g.ArtistId == userId &&
							g.DateTime > DateTime.UtcNow &&
							!g.IsCanceled)
				.Include(g => g.Genre)
				.ToList();

			return View(gigs);
		}

		[HttpGet]
		public IActionResult Attending()
		{
			var userId = User.GetUserId();
			var gigs = _context.Attendances
				.Where(a => a.AttendeeId == userId)
				.Select(a => a.Gig)
				.Include(g => g.Artist)
				.Include(g => g.Genre)
				.ToList();

			var viewModel = new GigsViewModel
			{
				UpcomingGigs = gigs,
				ShowActions = User.Identity.IsAuthenticated,
				Heading = "Gigs I'm attending"
			};

			return View("Gigs", viewModel);
		}
	}
}
