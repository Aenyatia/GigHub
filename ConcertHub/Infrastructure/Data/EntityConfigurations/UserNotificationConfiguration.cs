using ConcertHub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConcertHub.Infrastructure.Data.EntityConfigurations
{
	public class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
	{
		public void Configure(EntityTypeBuilder<UserNotification> builder)
		{
			builder.HasKey(n => new { n.ArtistId, n.NotificationId });

			builder.HasOne(n => n.Artist)
				.WithMany(a => a.UserNotifications)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
