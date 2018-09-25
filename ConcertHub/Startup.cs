using GigHub.Infrastructure.Persistence.Data;
using GigHub.Infrastructure.Persistence.Identity;
using GigHub.Web.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GigHub
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
			//.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

			services.AddDbContext<ApplicationContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("AppConnection")));

			services.AddDbContext<IdentityContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

			services.AddIdentity<AppUser, IdentityRole>(options =>
					options.User.RequireUniqueEmail = true)
				.AddEntityFrameworkStores<IdentityContext>()
				.AddDefaultTokenProviders();

			services.AddSingleton(AutoMapperMaps.Register());
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
