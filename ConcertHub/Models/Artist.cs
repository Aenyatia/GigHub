using System.ComponentModel.DataAnnotations;

namespace ConcertHub.Models
{
	public class Artist
	{
		[Required]
		public string Id { get; set; }
	}
}
