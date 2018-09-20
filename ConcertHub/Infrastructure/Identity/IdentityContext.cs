using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConcertHub.Infrastructure.Identity
{
	public class IdentityContext : IdentityDbContext<ApplicationUser>
	{
		public IdentityContext(DbContextOptions<IdentityContext> options)
			: base(options)
		{
		}
	}
}
