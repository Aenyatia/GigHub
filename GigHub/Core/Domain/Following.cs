namespace GigHub.Core.Domain
{
	// many to many
	public class Following
	{
		public string FollowerId { get; set; }
		public User Follower { get; set; }

		public string FolloweeId { get; set; }
		public User Followee { get; set; }
	}
}
