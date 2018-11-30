using GigHub.Core.Domain;
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
	public class GigsController : Controller
	{
		private readonly ApplicationContext _context;

		public GigsController(ApplicationContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Create()
		{
			var viewModel = new GigFormViewModel
			{
				Genres = _context.Genres.ToList(),
				Heading = "Add a Gig "
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

		[HttpGet("{gigId}")]
		public IActionResult Edit(int gigId)
		{
			var userId = User.GetUserId();
			var gig = _context.Gigs
				.Include(g => g.Artist)
				.Include(g => g.Genre)
				.SingleOrDefault(g => g.Id == gigId && g.ArtistId == userId);

			if (gig == null)
				return NotFound();

			var viewModel = new GigFormViewModel
			{
				Genres = _context.Genres.ToList(),
				Venue = gig.Venue,
				Date = gig.DateTime.ToString("yyyy-MM-dd"),
				Time = gig.DateTime.ToString("HH:mm"),
				GenreId = gig.GenreId,
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
				.Include(g => g.Attendances).ThenInclude(a => a.Attendee)
				.SingleOrDefault(g => g.Id == viewModel.Id && g.ArtistId == userId);

			if (gig == null)
				return NotFound();

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
			var userUpcomingGigs = _context.Attendances
				.Where(a => a.AttendeeId == userId)
				.Select(a => a.Gig)
				.Include(g => g.Artist)
				.Include(g => g.Genre)
				.ToList();

			var userAttendance = _context.Attendances
				.Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.UtcNow)
				.ToLookup(a => a.GigId);

			var viewModel = new GigsViewModel
			{
				UpcomingGigs = userUpcomingGigs,
				UserAttendance = userAttendance,

				ShowActions = User.Identity.IsAuthenticated,
				Heading = "Gigs I'm attending "
			};

			return View("Gigs", viewModel);
		}

		[HttpPost]
		public IActionResult Search(GigsViewModel viewModel)
		{
			return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
		}

		[HttpGet("{gigId}")]
		[AllowAnonymous]
		public IActionResult Details(int gigId)
		{
			var gig = _context.Gigs
				.Include(g => g.Artist)
				.Include(g => g.Genre)
				.SingleOrDefault(g => g.Id == gigId);

			if (gig == null)
				return NotFound();

			var viewModel = new GigDetailsViewModel { Gig = gig };

			if (User.Identity.IsAuthenticated)
			{
				var userId = User.GetUserId();

				viewModel.IsAttending = _context.Attendances
					.SingleOrDefault(a => a.AttendeeId == userId && a.GigId == gigId) != null;

				viewModel.IsFollowing = _context.Followings
					.SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == gig.ArtistId) != null;
			}

			return View("Details", viewModel);
		}
	}
}
