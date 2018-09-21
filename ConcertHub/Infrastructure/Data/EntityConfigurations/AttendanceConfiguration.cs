using ConcertHub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConcertHub.Infrastructure.Data.EntityConfigurations
{
	public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
	{
		public void Configure(EntityTypeBuilder<Attendance> builder)
		{
			builder.HasKey(a => new { a.GigId, a.AttendeeId });

			builder.HasOne(a => a.Gig)
				.WithMany()
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
