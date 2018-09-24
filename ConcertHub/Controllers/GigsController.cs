using ConcertHub.Extensions;
using ConcertHub.Models;
using ConcertHub.Persistence;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ConcertHub.Controllers
{
	[Authorize]
	public class GigsController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public GigsController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public IActionResult Create()
		{
			var viewModel = new GigFormViewModel
			{
				Genres = _unitOfWork.Genres.GetGenres(),
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
				viewModel.Genres = _unitOfWork.Genres.GetGenres();
				return View("GigForm", viewModel);
			}

			var gig = new Gig
			{
				ArtistId = User.GetUserId(),
				DateTime = viewModel.GetDateTime(),
				GenreId = viewModel.GenreId,
				Venue = viewModel.Venue
			};

			_unitOfWork.Gigs.Add(gig);
			_unitOfWork.Complete();

			return RedirectToAction("Mine", "Gigs");
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			var gig = _unitOfWork.Gigs.GetGig(id);

			if (gig == null)
				return NotFound();

			if (gig.ArtistId != User.GetUserId())
				return Unauthorized();

			var viewModel = new GigFormViewModel
			{
				Genres = _unitOfWork.Genres.GetGenres(),
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
				viewModel.Genres = _unitOfWork.Genres.GetGenres();
				return View("GigForm", viewModel);
			}

			var gig = _unitOfWork.Gigs.GetGigWithAttendees(viewModel.Id);

			if (gig == null)
				return NotFound();

			if (gig.ArtistId != User.GetUserId())
				return Unauthorized();

			gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.GenreId);

			_unitOfWork.Complete();

			return RedirectToAction("Mine", "Gigs");
		}

		[HttpGet]
		public IActionResult Mine()
		{
			var userId = User.GetUserId();
			var gigs = _unitOfWork.Gigs.GetUpcomingGigsByArtist(userId);

			return View(gigs);
		}

		[HttpGet]
		public IActionResult Attending()
		{
			var userId = User.GetUserId();

			var viewModel = new GigsViewModel
			{
				UpcomingGigs = _unitOfWork.Gigs.GetGigsUserAttending(userId),
				ShowActions = User.Identity.IsAuthenticated,
				Heading = "Gigs I'm attending",
				Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(a => a.GigId)
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
			var gig = _unitOfWork.Gigs.GetGigWithArtistAndGenre(gigId);

			if (gig == null)
				return NotFound();

			var viewModel = new GigDetailsViewModel { Gig = gig };

			if (User.Identity.IsAuthenticated)
			{
				var userId = User.GetUserId();

				viewModel.IsAttending = _unitOfWork.Attendances.GetAttendance(gig.Id, userId) != null;

				viewModel.IsFollowing = _unitOfWork.Followings.GetFollowing(userId, gig.ArtistId) != null;
			}

			return View("Details", viewModel);
		}
	}
}
