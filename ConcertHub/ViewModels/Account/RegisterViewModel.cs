using System.ComponentModel.DataAnnotations;

namespace ConcertHub.ViewModels.Account
{
	public class RegisterViewModel
	{
		//[Required(ErrorMessage = "The Username field is required. ")]
		public string UserName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
