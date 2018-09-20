﻿using ConcertHub.Infrastructure.Identity;
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
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Register(RegisterViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			var user = new User
			{
				UserName = viewModel.UserName,
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
		public async Task<IActionResult> Login(LoginViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			var user = await _userManager.FindByEmailAsync(viewModel.Email);
			if (user == null)
			{
				ModelState.AddModelError(nameof(viewModel.Email), "Invalid username or password.");
				return View(viewModel);
			}

			await _signInManager.SignOutAsync();
			var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, false, false);

			if (result.Succeeded)
				return RedirectToAction("Index", "Home");

			ModelState.AddModelError(nameof(viewModel.Email), "Invalid username or password.");
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