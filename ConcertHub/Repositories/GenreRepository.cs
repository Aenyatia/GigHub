using ConcertHub.Infrastructure.Data;
using ConcertHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace ConcertHub.Repositories
{
	public class GenreRepository : IGenreRepository
	{
		private readonly ConcertContext _context;

		public GenreRepository(ConcertContext context)
		{
			_context = context;
		}

		public IEnumerable<Genre> GetGenres()
		{
			return _context.Genres.ToList();
		}
	}
}
