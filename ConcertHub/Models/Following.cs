namespace ConcertHub.Models
{
	public class Following
	{
		public string FollowerId { get; set; }
		public Artist Follower { get; set; }

		public string FolloweeId { get; set; }
		public Artist Followee { get; set; }
	}
}
