using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entity.Identity;
using Talabat.Repository.Identity;

namespace Talabat.Extensions
{
    public static class AddIdentityUser
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddIdentity<AppUser, IdentityRole>(option =>
            {

            }).AddEntityFrameworkStores<AppUserIdentityDbContext>();
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(
                option =>{
                    option.TokenValidationParameters = new TokenValidatedParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                    };
                });

            return services;
        }
    }

    internal class TokenValidatedParameters : TokenValidationParameters
    {
    }
}
