using ConcertHub.Extensions;
using ConcertHub.Infrastructure.Data;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ConcertHub.Controllers
{
	public class ArtistsController : Controller
	{
		private readonly ConcertContext _context;

		public ArtistsController(ConcertContext context)
		{
			_context = context;
		}

		public IActionResult Following()
		{
			var userId = User.GetUserId();
			var artists = _context.Followings
				.Where(f => f.FollowerId == userId)
				.Select(f => f.Followee)
				.ToList();

			var viewModel = new FollowsViewModel
			{
				Artists = artists
			};

			return View(viewModel);
		}
	}
}
