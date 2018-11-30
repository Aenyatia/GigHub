using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GigHub.Infrastructure.Persistence.Identity
{
	public class IdentityDbContext : IdentityDbContext<ApplicationUser>
	{
		public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
			: base(options)
		{
		}
	}
}
