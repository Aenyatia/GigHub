using GigHub.Core.Domain;
using GigHub.Infrastructure.Persistence.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Infrastructure.Persistence.Data
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Gig> Gigs { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Following> Followings { get; set; }
		public DbSet<Attendance> Attendances { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<UserNotification> UserNotifications { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new GigConfiguration());
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new GenreConfiguration());
			modelBuilder.ApplyConfiguration(new FollowingConfiguration());
			modelBuilder.ApplyConfiguration(new AttendanceConfiguration());
			modelBuilder.ApplyConfiguration(new NotificationConfiguration());
			modelBuilder.ApplyConfiguration(new UserNotificationConfiguration());
		}
	}
}
