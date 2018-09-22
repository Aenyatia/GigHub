using ConcertHub.Infrastructure.Data.EntityConfigurations;
using ConcertHub.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertHub.Infrastructure.Data
{
	public class ConcertContext : DbContext
	{
		public DbSet<Gig> Gigs { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Artist> Artists { get; set; }
		public DbSet<Attendance> Attendances { get; set; }
		public DbSet<Following> Followings { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<UserNotification> UserNotifications { get; set; }

		public ConcertContext(DbContextOptions<ConcertContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new AttendanceConfiguration());
			modelBuilder.ApplyConfiguration(new FollowingConfiguration());
			modelBuilder.ApplyConfiguration(new UserNotificationConfiguration());

			modelBuilder.Entity<Genre>().HasData(
				new Genre { Id = 1, Name = "Jazz" },
				new Genre { Id = 2, Name = "Pop" },
				new Genre { Id = 3, Name = "Rock" },
				new Genre { Id = 4, Name = "JPop" },
				new Genre { Id = 5, Name = "Latin" },
				new Genre { Id = 6, Name = "Country" }
			);
		}
	}
}
