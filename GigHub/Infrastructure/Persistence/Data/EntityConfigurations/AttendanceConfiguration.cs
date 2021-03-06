﻿using GigHub.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigHub.Infrastructure.Persistence.Data.EntityConfigurations
{
	public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
	{
		public void Configure(EntityTypeBuilder<Attendance> builder)
		{
			builder.HasKey(a => new { a.GigId, a.AttendeeId });

			builder.HasOne(a => a.Gig)
				.WithMany(g => g.Attendances)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
