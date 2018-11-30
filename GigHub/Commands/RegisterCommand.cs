using System.ComponentModel.DataAnnotations;

namespace GigHub.Commands
{
	public class RegisterCommand
	{
		[Required]
		public string Name { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
