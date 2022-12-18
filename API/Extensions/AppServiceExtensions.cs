using API.Errors;
using Core.Entities.Identity;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extensions
{
    public static class AppServiceExtensions
    {
        public static async Task ConfigureDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = services.GetRequiredService<AppDbContext>();

                    await context.Database.MigrateAsync();

                    await AppDbContextSeed.SeedAsync(context,loggerFactory);

                    var userManager = services.GetRequiredService<UserManager<AppUser>>();

                    var identityContext = services.GetRequiredService<AppIdentityDbContext>();

                    await identityContext.Database.MigrateAsync();

                    await AppUserSeed.SeedUserAsync(userManager);
                    
                }

                catch(Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();

                    logger.LogError(ex, "An error occurred during migration");
                }
            }
        }

        public static void AddApiBehaviorOptions(this IServiceCollection service) =>
            service.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Error = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

        public static void AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            var builder = services.AddIdentityCore<AppUser>();

            builder = new IdentityBuilder(builder.UserType, builder.Services);

            builder.AddEntityFrameworkStores<AppIdentityDbContext>();

            builder.AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                        ValidIssuer = config["Token:Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false
                    };
                });
        }
    }
}
