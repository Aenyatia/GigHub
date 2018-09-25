using FluentValidation;
using GigHub.Web.ViewModels.Account;

namespace GigHub.Web.Validations
{
	public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
	{
		public RegisterViewModelValidator()
		{
			RuleFor(p => p.Name).NotNull().WithMessage("Pole 'Username' nie może być puste.");
			RuleFor(p => p.Email).NotNull().EmailAddress();
			RuleFor(p => p.Password).NotNull();
		}
	}
}
