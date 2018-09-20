using Microsoft.AspNetCore.Mvc;

namespace ConcertHub.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
	}
}
