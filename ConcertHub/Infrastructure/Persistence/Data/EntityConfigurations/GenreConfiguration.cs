using GigHub.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigHub.Infrastructure.Persistence.Data.EntityConfigurations
{
	public class GenreConfiguration : IEntityTypeConfiguration<Genre>
	{
		public void Configure(EntityTypeBuilder<Genre> builder)
		{
			builder.HasKey(g => g.Id);

			builder.Property(g => g.Name)
				.IsRequired();

			builder.HasData(
				new Genre(1, "Jazz"),
				new Genre(2, "Pop"),
				new Genre(3, "Rock"),
				new Genre(4, "JPop"),
				new Genre(5, "Latin"),
				new Genre(6, "Country")
			);
		}
	}
}
