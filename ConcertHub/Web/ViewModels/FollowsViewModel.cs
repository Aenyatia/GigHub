using GigHub.Core.Domain;
using System.Collections.Generic;

namespace GigHub.Web.ViewModels
{
	public class FollowsViewModel
	{
		public IEnumerable<User> Artists { get; set; }
	}
}
