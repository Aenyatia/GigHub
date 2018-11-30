using System.ComponentModel.DataAnnotations;

namespace GigHub.Commands
{
	public class LogInCommand
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
