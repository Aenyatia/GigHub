using ConcertHub.Infrastructure.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ConcertHub.Models
{
	public class Gig
	{
		public int Id { get; set; }

		[Required]
		public User Artist { get; set; }

		public DateTime DateTime { get; set; }

		[Required]
		[StringLength(255)]
		public string Venue { get; set; }

		[Required]
		public Genre Genre { get; set; }
	}
}
