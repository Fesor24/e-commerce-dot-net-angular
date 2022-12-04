using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if(!context.Genres.Any()) 
                {
                    var genreData = File.ReadAllText("../Infrastructure/Data/SeedData/genre.json");

                    var genre = JsonSerializer.Deserialize<List<Genre>>(genreData);

                    foreach(var item in genre)
                    {
                        context.Genres.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.ConsoleDevices.Any())
                {
                    var deviceData = File.ReadAllText("../Infrastructure/Data/SeedData/device.json");

                    var device = JsonSerializer.Deserialize<List<ConsoleDevice>>(deviceData);

                    foreach (var item in device)
                    {
                        context.ConsoleDevices.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.Games.Any())
                {
                    var gamesData = File.ReadAllText("../Infrastructure/Data/SeedData/games.json");

                    var games = JsonSerializer.Deserialize<List<Games>>(gamesData);

                    foreach (var item in games)
                    {
                        context.Games.Add(item);
                    }

                    await context.SaveChangesAsync(); 
                }
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<AppDbContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
