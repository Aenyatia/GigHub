using ConcertHub.Infrastructure.Data;
using ConcertHub.Models;
using System.Linq;

namespace ConcertHub.Repositories
{
	public class FollowingRepository
	{
		private readonly ConcertContext _context;

		public FollowingRepository(ConcertContext context)
		{
			_context = context;
		}

		public Following GetFollowing(string userId, string artistId)
		{
			return _context.Followings
				.SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == artistId);
		}
	}
}
