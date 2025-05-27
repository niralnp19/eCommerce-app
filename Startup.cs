using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalRChat.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddDefaultUI()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, ApplicationDbContext applicationDbContext)
        {
            applicationDbContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            CreateUserRoles(serviceProvider).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "ad",
                    pattern: "{controller=Ad}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "user",
                    pattern: "{controller=User}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHub<ChatHub>("/chatHub");
            });

        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Check if roles are already created
            if (!(await RoleManager.RoleExistsAsync("Admin")))
            {
                await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!(await RoleManager.RoleExistsAsync("User")))
            {
                await RoleManager.CreateAsync(new IdentityRole("User"));
            }

            // Check for admin account. Create if it doesn't exist
            var adminUser = await UserManager.FindByEmailAsync("admin@admin.com");
            if (adminUser == null)
            {
                var admin = new ApplicationUser
                {

                    UserName = "admin@admin.com",
                    Email = Configuration["admin@admin.com"],
                };

                // Temporary password
                var result = await UserManager.CreateAsync(admin, "Admin!1");
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
