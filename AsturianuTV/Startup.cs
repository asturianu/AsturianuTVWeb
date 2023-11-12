using AsturianuTV.Infrastructure.Data;
using AsturianuTV.Infrastructure.Interfaces;
using AsturianuTV.Infrastructure.Repository;
using AsturianuTV.Services;
using AsturianuTV.Services.Cybersports;
using AsturianuTV.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AsturianuTV
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AsturianuTVDbContext>(
                options => options.UseSqlServer(connection));
            services.AddControllersWithViews();
            services.AddAutoMapper(typeof(Startup));
            services.AddHttpContextAccessor();

            services.AddScoped<IPreloadDataService, PreloadDataService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ILeagueService, LeagueService>();
            services.AddScoped<IHeroeService, HeroeService>();

    #if DEBUG
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
    #endif
            services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
