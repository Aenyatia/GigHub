using System.ComponentModel.DataAnnotations;

namespace GigHub.Web.ViewModels.Account
{
	public class LogInViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
