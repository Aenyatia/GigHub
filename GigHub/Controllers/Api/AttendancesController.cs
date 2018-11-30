using GigHub.Core.Domain;
using GigHub.Infrastructure.Extensions;
using GigHub.Infrastructure.Persistence.Data;
using GigHub.Web.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GigHub.Controllers.Api
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class AttendancesController : ControllerBase
	{
		private readonly ApplicationDbContext _dbContext;

		public AttendancesController(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpPost]
		public IActionResult Attend([FromBody]AttendanceDto dto)
		{
			var userId = User.GetUserId();

			if (_dbContext.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId))
				return BadRequest("The attendance already exists.");

			var attendance = new Attendance
			{
				GigId = dto.GigId,
				AttendeeId = User.GetUserId()
			};

			_dbContext.Attendances.Add(attendance);
			_dbContext.SaveChanges();

			return Ok();
		}

		[HttpDelete("{gigId}")]
		public IActionResult DeleteAttendance(int gigId)
		{
			var userId = User.GetUserId();
			var attendance = _dbContext.Attendances.SingleOrDefault(a => a.GigId == gigId && a.AttendeeId == userId);

			if (attendance == null)
				return NotFound();

			_dbContext.Attendances.Remove(attendance);
			_dbContext.SaveChanges();

			return Ok(gigId);
		}
	}
}
