using ConcertHub.ViewModels.Account;
using FluentValidation;

namespace ConcertHub.Validations
{
	public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
	{
		public RegisterViewModelValidator()
		{
			RuleFor(p => p.UserName).NotNull().WithMessage("Pole 'Username' nie może być puste.");
			RuleFor(p => p.Email).NotNull().EmailAddress();
			RuleFor(p => p.Password).NotNull();
		}
	}
}
