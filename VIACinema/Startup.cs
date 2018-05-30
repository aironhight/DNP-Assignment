using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VIACinema.Data;
using VIACinema.Services;

namespace VIACinema
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
//            services.AddDbContext<ApplicationDbContext>(options =>
//                options.UseSqlServer(Configuration.GetConnectionString("ModelContext")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                    options.Conventions.AuthorizeFolder("/Tickets");
                });

            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailSender, EmailSender>();

//            services.AddDbContext<ApplicationDbContext>(options =>
//                    options.UseSqlServer(Configuration.GetConnectionString("ApplicationDbContext")));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Data Source=Cinema.db"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc();

            CreateUserRoles(services).Wait();
        }

        /// <summary>
        /// Create user roles on startup, if they don't already exist
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { "Admin" };

            foreach (var role in roles)
            {
                if (!await RoleManager.RoleExistsAsync(role))
                {
                    await RoleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}