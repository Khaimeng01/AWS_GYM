using System;
using Assigment.Areas.Identity.Data;
using Assigment.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Assigment.Areas.Identity.IdentityHostingStartup))]
namespace Assigment.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AssigmentContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AssigmentContextConnection")));

                services.AddDefaultIdentity<AssigmentUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<AssigmentContext>();
            });
        }
    }
}