using ConcertHub.Infrastructure.Data;
using ConcertHub.Infrastructure.Identity;
using ConcertHub.Models;
using ConcertHub.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConcertHub.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly ConcertContext _context;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ConcertContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			var user = new ApplicationUser
			{
				UserName = viewModel.Email,
				Email = viewModel.Email
			};

			var identityResult = await _userManager.CreateAsync(user, viewModel.Password);
			if (!identityResult.Succeeded)
			{
				foreach (var error in identityResult.Errors)
					ModelState.AddModelError(error.Code, error.Description);

				return View(viewModel);
			}

			var registeredUser = await _userManager.FindByEmailAsync(viewModel.Email);
			var artist = new Artist { Id = registeredUser.Id, Name = viewModel.UserName };
			_context.Artists.Add(artist);
			_context.SaveChanges();

			return RedirectToAction("Login");
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			var user = await _userManager.FindByEmailAsync(viewModel.Email);
			if (user == null)
			{
				ModelState.AddModelError(string.Empty, "Invalid username or password.");
				return View(viewModel);
			}

			await _signInManager.SignOutAsync();
			var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, false, false);

			if (result.Succeeded)
				return RedirectToAction("Index", "Home");

			ModelState.AddModelError(string.Empty, "Invalid username or password.");
			return View(viewModel);
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
