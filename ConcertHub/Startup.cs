using ConcertHub.Infrastructure.Data;
using ConcertHub.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConcertHub
{
	public class Startup
	{
		private IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddDbContext<ConcertContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("ConcertConnection")));

			services.AddDbContext<IdentityContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
					options.User.RequireUniqueEmail = true)
				.AddEntityFrameworkStores<IdentityContext>()
				.AddDefaultTokenProviders();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();
			app.UseAuthentication();
			app.UseMvc(route =>
			{
				route.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
