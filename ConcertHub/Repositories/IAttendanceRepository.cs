using System.Collections.Generic;
using ConcertHub.Models;

namespace ConcertHub.Repositories
{
	public interface IAttendanceRepository
	{
		IEnumerable<Attendance> GetFutureAttendances(string userId);
		Attendance GetAttendance(int gigId, string userId);
	}
}