﻿// <auto-generated />
using System;
using ConcertHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConcertHub.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ConcertContext))]
    partial class ConcertContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ConcertHub.Models.Artist", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("ConcertHub.Models.Attendance", b =>
                {
                    b.Property<int>("GigId");

                    b.Property<string>("AttendeeId");

                    b.HasKey("GigId", "AttendeeId");

                    b.HasIndex("AttendeeId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("ConcertHub.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new { Id = 1, Name = "Jazz" },
                        new { Id = 2, Name = "Pop" },
                        new { Id = 3, Name = "Rock" },
                        new { Id = 4, Name = "JPop" },
                        new { Id = 5, Name = "Latin" },
                        new { Id = 6, Name = "Country" }
                    );
                });

            modelBuilder.Entity("ConcertHub.Models.Gig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArtistId")
                        .IsRequired();

                    b.Property<DateTime>("DateTime");

                    b.Property<int>("GenreId");

                    b.Property<string>("Venue")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("GenreId");

                    b.ToTable("Gigs");
                });

            modelBuilder.Entity("ConcertHub.Models.Attendance", b =>
                {
                    b.HasOne("ConcertHub.Models.Artist", "Attendee")
                        .WithMany()
                        .HasForeignKey("AttendeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ConcertHub.Models.Gig", "Gig")
                        .WithMany()
                        .HasForeignKey("GigId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ConcertHub.Models.Gig", b =>
                {
                    b.HasOne("ConcertHub.Models.Artist", "Artist")
                        .WithMany()
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ConcertHub.Models.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
