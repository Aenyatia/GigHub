using GigHub.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Web.ViewModels
{
	public class GigFormViewModel
	{
		public int Id { get; set; }

		[Required]
		public string Venue { get; set; }

		[Required]
		[FutureDate]
		public string Date { get; set; }

		[Required]
		public string Time { get; set; }

		[Required]
		public int GenreId { get; set; }

		public IEnumerable<Genre> Genres { get; set; }

		public string Heading { get; set; }

		public DateTime GetDateTime()
			=> DateTime.Parse($"{Date} {Time}");
	}
}
