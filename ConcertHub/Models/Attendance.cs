namespace ConcertHub.Models
{
	public class Attendance
	{
		public int GigId { get; set; }
		public Gig Gig { get; set; }

		public string AttendeeId { get; set; }
		public Artist Attendee { get; set; }
	}
}
