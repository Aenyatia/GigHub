using ConcertHub.Infrastructure.Data;
using ConcertHub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ConcertHub.Controllers
{
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
	}
}
