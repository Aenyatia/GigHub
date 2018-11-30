using GigHub.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GigHub.Commands;

namespace GigHub.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody]LogInCommand viewModel)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var user = await _userManager.FindByEmailAsync(viewModel.Email);
			if (user == null)
			{
				ModelState.AddModelError(string.Empty, "Invalid username or password.");
				return BadRequest();
			}

			await _signInManager.SignOutAsync();
			var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, false, false);

			if (result.Succeeded)
				return Ok("Sign in");

			ModelState.AddModelError(string.Empty, "Invalid username or password.");
			return BadRequest();
		}
	}
}
