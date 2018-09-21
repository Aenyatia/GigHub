using ConcertHub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConcertHub.Infrastructure.Data.EntityConfigurations
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
