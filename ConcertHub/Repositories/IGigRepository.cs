using ConcertHub.Models;
using System.Collections.Generic;

namespace ConcertHub.Repositories
{
	public interface IGigRepository
	{
		Gig GetGig(int gigId);
		Gig GetGigWithArtistAndGenre(int gigId);
		Gig GetGigWithAttendees(int gigId);
		IEnumerable<Gig> GetGigsUserAttending(string userId);
		IEnumerable<Gig> GetUpcomingGigsByArtist(string userId);
		void Add(Gig gig);
	}
}
