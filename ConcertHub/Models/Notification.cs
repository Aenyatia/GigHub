using System;
using System.ComponentModel.DataAnnotations;

namespace ConcertHub.Models
{
	public class Notification
	{
		public int Id { get; private set; }
		public DateTime DateTime { get; private set; }
		public NotificationType Type { get; private set; }
		public DateTime? OriginalDateTime { get; private set; }
		public string OriginalValue { get; private set; }

		[Required]
		public Gig Gig { get; private set; }

		protected Notification()
		{
			// required by EF
		}

		private Notification(NotificationType type, Gig gig)
		{
			Gig = gig ?? throw new ArgumentNullException(nameof(gig));

			Type = type;
			DateTime = DateTime.UtcNow;
		}

		public static Notification GigCreated(Gig gig)
		{
			return new Notification(NotificationType.GigCreated, gig);
		}

		public static Notification GigUpdated(Gig newGig, DateTime originalDateTime, string originalVenue)
		{
			var notification = new Notification(NotificationType.GigUpdated, newGig)
			{
				OriginalDateTime = originalDateTime,
				OriginalValue = originalVenue
			};

			return notification;
		}

		public static Notification GigCanceled(Gig gig)
		{
			return new Notification(NotificationType.GigCanceled, gig);
		}
	}
}
