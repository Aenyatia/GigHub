using GigHub.Core.Domain;
using GigHub.Infrastructure.Persistence.Data;
using GigHub.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GigHub.Commands;

namespace GigHub.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly ApplicationDbContext _dbContext;

		public AccountController(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			ApplicationDbContext dbContext)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_dbContext = dbContext;
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register() => View();

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterCommand command)
		{
			if (!ModelState.IsValid)
				return View(command);

			var user = new ApplicationUser
			{
				UserName = command.Email,
				Email = command.Email
			};

			var identityResult = await _userManager.CreateAsync(user, command.Password);
			if (!identityResult.Succeeded)
			{
				foreach (var error in identityResult.Errors)
					ModelState.AddModelError(error.Code, error.Description);

				return View(command);
			}

			// create local user - need to refactoring
			var identityUser = await _userManager.FindByEmailAsync(command.Email);
			var localUser = new User(identityUser.Id, command.Name);
			_dbContext.Users.Add(localUser);
			_dbContext.SaveChanges();

			return RedirectToAction("Login");
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login() => View();

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LogInCommand viewModel)
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
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
