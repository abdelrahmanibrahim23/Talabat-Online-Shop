using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity.Identity;
using Talabat.Core.Services;

namespace Talabat.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration Configuration;

        public TokenService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async Task<string> CreatToken(AppUser user, UserManager<AppUser> userManager)
        {
            var authClaim=new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email ),
                new Claim(ClaimTypes.Name,user.DisplayName)
            };
            var authRols = await userManager.GetRolesAsync(user);
            foreach (var role in authRols)
                authClaim.Add(new Claim(ClaimTypes.Role,role));
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]));
            var token = new JwtSecurityToken(

                issuer: Configuration["JWT:ValidIssuer"],
                audience: Configuration["JWT:ValidAudience"],
                expires:DateTime.Now.AddDays(30),
                claims:authClaim,
                signingCredentials:new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
            
            

        }
    }
}
