﻿using System.ComponentModel.DataAnnotations;

namespace ConcertHub.ViewModels.Account
{
	public class RegisterViewModel
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
