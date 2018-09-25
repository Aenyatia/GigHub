using GigHub.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigHub.Infrastructure.Persistence.Data.EntityConfigurations
{
	public class FollowingConfiguration : IEntityTypeConfiguration<Following>
	{
		public void Configure(EntityTypeBuilder<Following> builder)
		{
			builder.HasKey(f => new { f.FollowerId, f.FolloweeId });

			builder.HasOne(f => f.Follower)
				.WithMany(u => u.Followees)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(f => f.Followee)
				.WithMany(u => u.Followers)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
