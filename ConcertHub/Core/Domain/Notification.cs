using System;

namespace GigHub.Core.Domain
{
	public class Notification
	{
		public int Id { get; protected set; }
		public NotificationType Type { get; protected set; }
		public Gig Gig { get; protected set; }
		public DateTime DateTime { get; protected set; }

		public DateTime? OriginalDateTime { get; protected set; }
		public string OriginalVenue { get; protected set; }

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

		public static Notification GigUpdated(Gig updatedGig, DateTime originalDateTime, string originalVenue)
		{
			return new Notification(NotificationType.GigUpdated, updatedGig)
			{
				OriginalDateTime = originalDateTime,
				OriginalVenue = originalVenue
			};
		}

		public static Notification GigCanceled(Gig gig)
		{
			return new Notification(NotificationType.GigCanceled, gig);
		}
	}
}
