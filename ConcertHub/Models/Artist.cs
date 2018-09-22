﻿using System.Collections.Generic;

namespace ConcertHub.Models
{
	public class Artist
	{
		public string Id { get; set; }
		public string Name { get; set; }

		public ICollection<Following> Followers { get; set; } = new List<Following>();
		public ICollection<Following> Followees { get; set; } = new List<Following>();
		public ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();

		public void Notify(Notification notification)
		{
			UserNotifications.Add(new UserNotification(this, notification));
		}
	}
}
