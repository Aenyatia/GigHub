using System;
using System.ComponentModel.DataAnnotations;

namespace ConcertHub.Models
{
	public class Notification
	{
		public int Id { get; private set; }
		public DateTime DateTime { get; private set; }
		public NotificationType Type { get; private set; }
		public DateTime? OriginalDateTime { get; set; }
		public string OriginalValue { get; set; }

		[Required]
		public Gig Gig { get; private set; }

		protected Notification()
		{
			// required by EF
		}

		public Notification(NotificationType type, Gig gig)
		{
			Gig = gig ?? throw new ArgumentNullException(nameof(gig));

			Type = type;
			DateTime = DateTime.UtcNow;
		}
	}
}
