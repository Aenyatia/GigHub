using ConcertHub.Extensions;
using ConcertHub.Infrastructure.Data;
using ConcertHub.Models;
using ConcertHub.Repositories;
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
		private readonly AttendanceRepository _attendanceRepository;
		private readonly GigRepository _gigRepository;
		private readonly GenreRepository _genreRepository;
		private readonly FollowingRepository _followingRepository;

		public GigsController(ConcertContext context)
		{
			_context = context;
			_attendanceRepository = new AttendanceRepository(_context);
			_gigRepository = new GigRepository(_context);
			_followingRepository = new FollowingRepository(_context);
			_genreRepository = new GenreRepository(_context);
		}

		[HttpGet]
		public IActionResult Create()
		{
			var viewModel = new GigFormViewModel
			{
				Genres = _genreRepository.GetGenres(),
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
				viewModel.Genres = _genreRepository.GetGenres();
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
			var gig = _gigRepository.GetGig(id);

			if (gig == null)
				return NotFound();

			if (gig.ArtistId != User.GetUserId())
				return Unauthorized();

			var viewModel = new GigFormViewModel
			{
				Genres = _genreRepository.GetGenres(),
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
				viewModel.Genres = _genreRepository.GetGenres();
				return View("GigForm", viewModel);
			}

			var gig = _gigRepository.GetGigWithAttendees(viewModel.Id);

			if (gig == null)
				return NotFound();

			if (gig.ArtistId != User.GetUserId())
				return Unauthorized();

			gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.GenreId);

			_context.SaveChanges();

			return RedirectToAction("Mine", "Gigs");
		}

		[HttpGet]
		public IActionResult Mine()
		{
			var userId = User.GetUserId();
			var gigs = _gigRepository.GetUpcomingGigsByArtist(userId);

			return View(gigs);
		}

		[HttpGet]
		public IActionResult Attending()
		{
			var userId = User.GetUserId();

			var viewModel = new GigsViewModel
			{
				UpcomingGigs = _gigRepository.GetGigsUserAttending(userId),
				ShowActions = User.Identity.IsAuthenticated,
				Heading = "Gigs I'm attending",
				Attendances = _attendanceRepository.GetFutureAttendances(userId).ToLookup(a => a.GigId)
			};

			return View("Gigs", viewModel);
		}

		[HttpPost]
		public IActionResult Search(GigsViewModel viewModel)
		{
			return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult Details(int gigId)
		{
			var gig = _gigRepository.GetGigWithArtistAndGenre(gigId);

			if (gig == null)
				return NotFound();

			var viewModel = new GigDetailsViewModel { Gig = gig };

			if (User.Identity.IsAuthenticated)
			{
				var userId = User.GetUserId();

				viewModel.IsAttending = _attendanceRepository.GetAttendance(gig.Id, userId) != null;

				viewModel.IsFollowing = _followingRepository.GetFollowing(userId, gig.ArtistId) != null;
			}

			return View("Details", viewModel);
		}
	}
}
