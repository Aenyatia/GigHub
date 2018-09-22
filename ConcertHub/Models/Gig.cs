using System;
using System.ComponentModel.DataAnnotations;

namespace ConcertHub.Models
{
	public class Gig
	{
		public int Id { get; set; }

		[Required]
		public string ArtistId { get; set; }
		public Artist Artist { get; set; }

		[Required]
		public DateTime DateTime { get; set; }

		[Required]
		[StringLength(255)]
		public string Venue { get; set; }

		[Required]
		public int GenreId { get; set; }
		public Genre Genre { get; set; }

		public bool IsCanceled { get; set; }
	}
}
