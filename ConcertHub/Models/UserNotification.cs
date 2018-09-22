namespace ConcertHub.Models
{
	public class UserNotification
	{
		public string ArtistId { get; set; }
		public Artist Artist { get; set; }

		public int NotificationId { get; set; }
		public Notification Notification { get; set; }

		public bool IsRead { get; set; }
	}
}
