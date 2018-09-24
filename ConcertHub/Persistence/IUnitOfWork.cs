using ConcertHub.Repositories;

namespace ConcertHub.Persistence
{
	public interface IUnitOfWork
	{
		IGigRepository Gigs { get; }
		IAttendanceRepository Attendances { get; }
		IGenreRepository Genres { get; }
		IFollowingRepository Followings { get; }
		void Complete();
	}
}