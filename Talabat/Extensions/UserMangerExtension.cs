using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.Core.Entity.Identity;

namespace Talabat.Extensions
{
    public static class UserMangerExtension
    {
        public static async Task<AppUser> FindUserEithAddress(this UserManager<AppUser> userManager,ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user= await userManager.Users.Include(U=>U.Address).SingleOrDefaultAsync(E=>E.Email==email);
            return user;
        }
    }
}
