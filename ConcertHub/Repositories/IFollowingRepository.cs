using ConcertHub.Models;

namespace ConcertHub.Repositories
{
	public interface IFollowingRepository
	{
		Following GetFollowing(string userId, string artistId);
	}
}