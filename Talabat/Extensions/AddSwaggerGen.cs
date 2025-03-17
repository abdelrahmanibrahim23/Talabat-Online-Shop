using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Talabat.Extensions
{
    public static class AddSwaggerGen
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services) 
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Talabat", Version = "v1" });
                
            });
            return services;    
        }
    }
}
