
using Assigment.Areas.Identity.Data;
using Assigment.Database;
using Assigment.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.EntityFrameworkCore;
using Assigment.Models;

namespace Assigment
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
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDbContext<MyDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("AssigmentContextConnection_2")));


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            CreateRoles(serviceProvider).GetAwaiter().GetResult();
            CreateAdminUser(serviceProvider).GetAwaiter().GetResult();

            //app.UseMiddleware<RoleBasedRedirectMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Create "Admin" role if it doesn't exist
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Create "Customer" role if it doesn't exist
            if (!await roleManager.RoleExistsAsync("Customer"))
            {
                await roleManager.CreateAsync(new IdentityRole("Customer"));
            }
        }

        private async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AssigmentUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Check if the "Admin" role exists, throw an exception if not
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                throw new InvalidOperationException("Admin role not found");
            }

            // Check if a user with the desired email exists
            var adminEmail = "admin@example.com";
            var admin = await userManager.FindByEmailAsync(adminEmail);

            // If the user doesn't exist, create it and assign the "Admin" role
            if (admin == null)
            {
                admin = new AssigmentUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    PhoneNumber = "0125031601"
                };


                var result =  await userManager.CreateAsync(admin,"1233456_Admin");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
                   
            }
        }
    }
}
