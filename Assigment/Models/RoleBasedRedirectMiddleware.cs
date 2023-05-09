using System;
using System.Threading.Tasks;
using Assigment.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;


namespace Assigment.Models
{
    public class RoleBasedRedirectMiddleware
    {
        private readonly RequestDelegate _next;

        public RoleBasedRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, UserManager<AssigmentUser> userManager, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var user = await userManager.GetUserAsync(context.User);
                var roles = await userManager.GetRolesAsync(user);

                if (roles.Contains("Admin"))
                {
                    if (context.Request.Path.StartsWithSegments("/", StringComparison.OrdinalIgnoreCase))
                    {
                        context.Response.Redirect("/ManageAdmin");
                        return;
                    }
                }
                else if (roles.Contains("Customer"))
                {
                    if (context.Request.Path.StartsWithSegments("/Home/Index", StringComparison.OrdinalIgnoreCase))
                    {
                        context.Response.Redirect("/Customer/Dashboard");
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
