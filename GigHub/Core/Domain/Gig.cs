using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Core.Domain
{
	public class Gig
	{
		public int Id { get; protected set; }

		public string ArtistId { get; set; }
		public User Artist { get; set; }

		public DateTime DateTime { get; set; }

		public string Venue { get; set; }

		public int GenreId { get; set; }
		public Genre Genre { get; set; }

		public bool IsCanceled { get; protected set; }

		public ICollection<Attendance> Attendances { get; protected set; } = new List<Attendance>();

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
