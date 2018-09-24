using ConcertHub.Dtos;
using ConcertHub.Extensions;
using ConcertHub.Infrastructure.Data;
using ConcertHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ConcertHub.Controllers.Api
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class AttendancesController : ControllerBase
	{
		private readonly ConcertContext _context;

		public AttendancesController(ConcertContext context)
		{
			_context = context;
		}

		[HttpPost]
		public IActionResult Attend([FromBody]AttendanceDto dto)
		{
			var userId = User.GetUserId();

			if (_context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId))
				return BadRequest("The attendance already exists.");

			var attendance = new Attendance
			{
				GigId = dto.GigId,
				AttendeeId = User.GetUserId()
			};

			_context.Attendances.Add(attendance);
			_context.SaveChanges();

			return Ok();
		}

		[HttpDelete("{gigId}")]
		public IActionResult DeleteAttendance(int gigId)
		{
			var userId = User.GetUserId();
			var attendance = _context.Attendances.SingleOrDefault(a => a.GigId == gigId && a.AttendeeId == userId);

			if (attendance == null)
				return NotFound();

			_context.Attendances.Remove(attendance);
			_context.SaveChanges();

			return Ok(gigId);
		}
	}
}
