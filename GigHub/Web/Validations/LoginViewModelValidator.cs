using FluentValidation;
using GigHub.Web.ViewModels.Account;

namespace GigHub.Web.Validations
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
