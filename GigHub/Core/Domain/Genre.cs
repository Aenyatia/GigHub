using GigHub.Core.Extensions;
using System;

namespace GigHub.Core.Domain
{
	public class Genre
	{
		public int Id { get; protected set; }
		public string Name { get; protected set; }

		protected Genre()
		{
			// required by EF
		}

		public Genre(int id, string value)
		{
			if (value.IsEmpty()) throw new ArgumentNullException(nameof(value));

			Id = id;
			Name = value;
		}
	}
}
