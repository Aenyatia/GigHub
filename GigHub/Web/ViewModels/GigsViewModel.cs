using GigHub.Core.Domain;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Web.ViewModels
{
	public class GigsViewModel
	{
		public string Heading { get; set; }
		public string SearchTerm { get; set; }

		public IEnumerable<Gig> UpcomingGigs { get; set; }
		public bool ShowActions { get; set; }

		public ILookup<int, Attendance> UserAttendance { get; set; }
	}
}
