﻿using GigHub.Core.Domain;

namespace GigHub.Web.ViewModels
{
	public class GigDetailsViewModel
	{
		public Gig Gig { get; set; }
		public bool IsAttending { get; set; }
		public bool IsFollowing { get; set; }
	}
}
