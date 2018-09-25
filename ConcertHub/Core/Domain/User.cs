using GigHub.Core.Extensions;
using System;
using System.Collections.Generic;

namespace GigHub.Core.Domain
{
	public class User
	{
		public string Id { get; protected set; }
		public string Name { get; protected set; }

		protected User()
		{
			// required by EF
		}

		public User(string id, string name)
		{
			if (id.IsEmpty()) throw new ArgumentNullException(nameof(id));
			if (name.IsEmpty()) throw new ArgumentNullException(nameof(name));

			Id = id;
			Name = name;
		}

		public ICollection<Following> Followers { get; set; } = new List<Following>();
		public ICollection<Following> Followees { get; set; } = new List<Following>();
		public ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();

		public void Notify(Notification notification)
		{
			UserNotifications.Add(new UserNotification(this, notification));
		}
	}
}
