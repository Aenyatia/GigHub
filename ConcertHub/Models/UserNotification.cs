using System;

namespace ConcertHub.Models
{
	public class UserNotification
	{
		public string ArtistId { get; private set; }
		public Artist Artist { get; private set; }

		public int NotificationId { get; private set; }
		public Notification Notification { get; private set; }

		public bool IsRead { get; private set; }

		protected UserNotification()
		{
		}

		public UserNotification(Artist artist, Notification notification)
		{
			Artist = artist ?? throw new ArgumentNullException(nameof(artist));
			Notification = notification ?? throw new ArgumentNullException(nameof(notification));
		}

		public void Read()
		{
			IsRead = true;
		}
	}
}
