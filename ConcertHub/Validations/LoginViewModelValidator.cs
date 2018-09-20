using ConcertHub.ViewModels.Account;
using FluentValidation;

namespace ConcertHub.Validations
{
	public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
	{
		public LoginViewModelValidator()
		{
			RuleFor(p => p.Email).NotNull().EmailAddress();
			RuleFor(p => p.Password).NotNull();
		}
	}
}
