using ConcertHub.Infrastructure.Data;
using ConcertHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConcertHub.Repositories
{
	public class AttendanceRepository
	{
		private readonly ConcertContext _context;

		public AttendanceRepository(ConcertContext context)
		{
			_context = context;
		}

		public IEnumerable<Attendance> GetFutureAttendances(string userId)
		{
			return _context.Attendances
				.Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.UtcNow)
				.ToList();
		}

		public Attendance GetAttendance(int gigId, string userId)
		{
			return _context.Attendances
				.SingleOrDefault(a => a.AttendeeId == userId && a.GigId == gigId);
		}
	}
}
