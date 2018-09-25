using System;

namespace GigHub.Core.Domain
{
	public class UserNotification
	{
		public string UserId { get; protected set; }
		public User User { get; protected set; }

		public int NotificationId { get; protected set; }
		public Notification Notification { get; protected set; }

		public bool IsRead { get; protected set; }

		protected UserNotification()
		{
			// required by EF
		}

		public UserNotification(User user, Notification notification)
		{
			User = user ?? throw new ArgumentNullException(nameof(user));
			Notification = notification ?? throw new ArgumentNullException(nameof(notification));
		}

		public void Read()
		{
			IsRead = true;
		}
	}
}
