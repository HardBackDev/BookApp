using BookAppServer.Exceptions;
using BookAppServer.Models;
using BookAppServer.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookAppServer.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            BadRequestException => StatusCodes.Status400BadRequest,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        await context.Response.WriteAsync(new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                        }.ToString());
                    }
                });
            });
        }

        public async static Task MigrateDB(this WebApplication app)
        {
            using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<RepositoryContext>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                context.Database.EnsureCreated();

                await AddingUsers(userManager);
            }
        }

        public async static Task AddingUsers(UserManager<User> userManager)
        {
            var adminHas = await userManager.FindByNameAsync("Admin");
            var userHas = await userManager.FindByNameAsync("User");

            if (adminHas is null)
            {
                var admin = new User
                {
                    PhoneNumber = "2-2-2-2",
                    Email = "Admin@gmail.com",
                    UserName = "Admin"
                };

                await userManager.CreateAsync(admin, "Password1000");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            if (userHas is null)
            {
                var user = new User()
                {
                    PhoneNumber = "1-1-1-1",
                    Email = "User@gmail.com",
                    UserName = "User"
                };

                await userManager.CreateAsync(user, "Password1000");
                await userManager.AddToRoleAsync(user, "User");
            }

        }
    }
}
