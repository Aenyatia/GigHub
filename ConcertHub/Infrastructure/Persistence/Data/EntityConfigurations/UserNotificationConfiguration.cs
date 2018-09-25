using GigHub.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigHub.Infrastructure.Persistence.Data.EntityConfigurations
{
	public class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
	{
		public void Configure(EntityTypeBuilder<UserNotification> builder)
		{
			builder.HasKey(n => new { ArtistId = n.UserId, n.NotificationId });

			builder.HasOne(n => n.User)
				.WithMany(a => a.UserNotifications)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
