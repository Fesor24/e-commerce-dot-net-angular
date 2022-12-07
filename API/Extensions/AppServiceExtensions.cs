using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}
