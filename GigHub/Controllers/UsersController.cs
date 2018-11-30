using GigHub.Infrastructure.Extensions;
using GigHub.Infrastructure.Persistence.Data;
using GigHub.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GigHub.Controllers
{
	public class UsersController : Controller
	{
		private readonly ApplicationContext _context;

		public UsersController(ApplicationContext context)
		{
			_context = context;
		}

		public IActionResult Following()
		{
			var userId = User.GetUserId();
			var users = _context.Followings
				.Where(f => f.FollowerId == userId)
				.Select(f => f.Followee)
				.ToList();

			var viewModel = new FollowsViewModel { Artists = users };

			return View(viewModel);
		}
	}
}
