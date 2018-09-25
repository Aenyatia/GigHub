using GigHub.Infrastructure.Persistence.Data;
using GigHub.Infrastructure.Persistence.Identity;
using GigHub.Web.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GigHub.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ApplicationContext _context;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationContext context)
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

			var user = new AppUser
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

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
