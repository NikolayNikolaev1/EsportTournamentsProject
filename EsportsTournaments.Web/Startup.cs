namespace EsportsTournaments.Web
{
    using Core.Extensions;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Services.Admin;
    using Services.Admin.Implementations;
    using Services.Implementations;
    using Services.Moderator;
    using Services.Moderator.Implentations;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EsportsTournamentsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<EsportsTournamentsDbContext>()
                .AddDefaultTokenProviders();

            //services.AddAutoMapper();

            services.AddTransient<IAdminUserService, AdminUserService>();
            services.AddTransient<IAdminGameService, AdminGameService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<ITournamentService, TournamentService>();
            services.AddTransient<IModeratorTournamentService, ModeratorTournamentService>();

            services.AddDomainService();

            services.AddMvc(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDatabaseMigration();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "area",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
