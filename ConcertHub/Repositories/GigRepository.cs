using ConcertHub.Infrastructure.Data;
using ConcertHub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConcertHub.Repositories
{
	public class GigRepository
	{
		private readonly ConcertContext _context;

		public GigRepository(ConcertContext context)
		{
			_context = context;
		}

		public Gig GetGig(int gigId)
		{
			return _context.Gigs
				.Include(g => g.Artist)
				.Include(g => g.Genre)
				.SingleOrDefault(g => g.Id == gigId);
		}

		public Gig GetGigWithArtistAndGenre(int gigId)
		{
			return _context.Gigs
				.Include(g => g.Artist)
				.Include(g => g.Genre)
				.SingleOrDefault(g => g.Id == gigId);
		}

		public Gig GetGigWithAttendees(int gigId)
		{
			return _context.Gigs
				.Include(g => g.Attendances)
				.ThenInclude(a => a.Attendee)
				.SingleOrDefault(g => g.Id == gigId);
		}

		public IEnumerable<Gig> GetGigsUserAttending(string userId)
		{
			return _context.Attendances
				.Where(a => a.AttendeeId == userId)
				.Select(a => a.Gig)
				.Include(g => g.Artist)
				.Include(g => g.Genre)
				.ToList();
		}

		public IEnumerable<Gig> GetUpcomingGigsByArtist(string userId)
		{
			return _context.Gigs
				.Where(g => g.ArtistId == userId &&
							g.DateTime > DateTime.UtcNow &&
							!g.IsCanceled)
				.Include(g => g.Genre)
				.ToList();
		}

		public void Add(Gig gig)
		{
			_context.Add(gig);
		}
	}
}
