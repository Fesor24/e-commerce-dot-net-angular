using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class AppUserSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Fesor",
                    Email = "fesor@mail.com",
                    UserName = "fesor@mail.com",
                    Address = new Address
                    {
                        FirstName = "Fesor",
                        LastName = "Wale",
                        Street = "10 Maryland",
                        City = "Ikeja",
                        State = "Lagos",
                        ZipCode = "324-212"
                    }
                };

                await userManager.CreateAsync(user, "Passw0rd_123");
            }
        }
    }
}
