using GigHub.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigHub.Infrastructure.Persistence.Data.EntityConfigurations
{
	public class GigConfiguration : IEntityTypeConfiguration<Gig>
	{
		public void Configure(EntityTypeBuilder<Gig> builder)
		{
			builder.HasKey(g => g.Id);

			builder.Property(g => g.ArtistId)
				.IsRequired();

			builder.Property(g => g.GenreId)
				.IsRequired();

			builder.Property(g => g.DateTime)
				.IsRequired();

			builder.Property(g => g.Venue)
				.IsRequired()
				.HasMaxLength(255);
		}
	}
}
