using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity.Identity;

namespace Talabat.Repository.Identity.DataSeeding
{
    public class AppUserSeeding 
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "Abdelrahman Ibrahim",
                    Email = "abdo.ibrahim@gmail.com",
                    UserName = "abdo.ibrahim",
                    PhoneNumber = "01125165465"

                };
                await userManager.CreateAsync(User, "Pa$$0wrd");
            }
        }
    }
}
