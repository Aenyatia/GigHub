using FluentValidation;
using GigHub.Web.ViewModels;
using System;

namespace GigHub.Web.Validations
{
	public class GigFormViewModelValidator : AbstractValidator<GigFormViewModel>
	{
		public GigFormViewModelValidator()
		{
			RuleFor(p => p.Venue).NotNull();
			RuleFor(p => p.Date).NotNull();
			RuleFor(p => p.Time).NotNull();
			RuleFor(p => p.GenreId).NotNull().WithMessage("Pole 'Genre' nie może być puste.");

			RuleFor(p => p.Date).Must(ValidDate).WithMessage("Proszę podać datę w przyszłości.");
			RuleFor(p => p.Time).Must(ValidTime);
		}

		public bool ValidDate(string date)
		{
			var isValid = DateTime.TryParse(date, out var dateTime);

			return (isValid && dateTime > DateTime.UtcNow);
		}

		public bool ValidTime(string time)
		{
			var isValid = DateTime.TryParse(time, out _);

			return isValid;
		}
	}
}
