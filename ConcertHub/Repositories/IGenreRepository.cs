using System.Collections.Generic;
using ConcertHub.Models;

namespace ConcertHub.Repositories
{
	public interface IGenreRepository
	{
		IEnumerable<Genre> GetGenres();
	}
}