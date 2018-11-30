namespace GigHub.Core.Domain
{
	// many to many
	public class Attendance
	{
		public int GigId { get; set; }
		public Gig Gig { get; set; }

		public string AttendeeId { get; set; }
		public User Attendee { get; set; }
	}
}
