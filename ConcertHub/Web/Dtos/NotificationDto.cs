using GigHub.Core.Domain;
using System;

namespace GigHub.Web.Dtos
{
	public class NotificationDto
	{
		public DateTime DateTime { get; set; }
		public NotificationType Type { get; set; }
		public DateTime? OriginalDateTime { get; set; }
		public string OriginalValue { get; set; }
		public GigDto Gig { get; set; }
	}
}
