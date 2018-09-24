using ConcertHub.Infrastructure.Data;
using ConcertHub.Repositories;

namespace ConcertHub.Persistence
{
	public class UnitOfWork
	{
		private readonly ConcertContext _context;

		public GigRepository Gigs { get; private set; }
		public AttendanceRepository Attendances { get; private set; }
		public GenreRepository Genres { get; private set; }
		public FollowingRepository Followings { get; private set; }

		public UnitOfWork(ConcertContext context)
		{
			_context = context;

			Gigs = new GigRepository(_context);
			Attendances = new AttendanceRepository(_context);
			Genres = new GenreRepository(_context);
			Followings = new FollowingRepository(_context);
		}

		public void Complete()
		{
			_context.SaveChanges();
		}
	}
}
