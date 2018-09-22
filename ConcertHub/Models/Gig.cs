using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

		public bool IsCanceled { get; private set; }

		public ICollection<Attendance> Attendances { get; private set; } = new List<Attendance>();

		public void Cancel()
		{
			IsCanceled = true;

			var notification = Notification.GigCanceled(this);

			foreach (var attendee in Attendances.Select(a => a.Attendee))
				attendee.Notify(notification);
		}

		public void Modify(DateTime dateTime, string venue, int genreId)
		{
			var notification = Notification.GigUpdated(this, DateTime, Venue);

			DateTime = dateTime;
			Venue = venue;
			GenreId = genreId;

			foreach (var attendee in Attendances.Select(a => a.Attendee))
				attendee.Notify(notification);
		}
	}
}
