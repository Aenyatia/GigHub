using ConcertHub.Infrastructure.Data;
using ConcertHub.Repositories;

namespace ConcertHub.Persistence
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ConcertContext _context;

		public IGigRepository Gigs { get; private set; }
		public IAttendanceRepository Attendances { get; private set; }
		public IGenreRepository Genres { get; private set; }
		public IFollowingRepository Followings { get; private set; }

		public UnitOfWork(ConcertContext context,
			IGigRepository gigRepository,
			IAttendanceRepository attendanceRepository,
			IGenreRepository genreRepository,
			IFollowingRepository followingRepository)
		{
			_context = context;

			Gigs = gigRepository;
			Attendances = attendanceRepository;
			Genres = genreRepository;
			Followings = followingRepository;
		}

		public void Complete()
		{
			_context.SaveChanges();
		}
	}
}
