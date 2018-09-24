using ConcertHub.Models;

namespace ConcertHub.ViewModels
{
	public class GigDetailsViewModel
	{
		public Gig Gig { get; set; }
		public bool IsAttending { get; set; }
		public bool IsFollowing { get; set; }
	}
}
