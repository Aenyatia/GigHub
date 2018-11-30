using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Web.Validators
{
	public class FutureDate : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			var isValid = DateTime.TryParse(Convert.ToString(value), out var dateTime);

			return (isValid && dateTime > DateTime.UtcNow);
		}
	}
}
