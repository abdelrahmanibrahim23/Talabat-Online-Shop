using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Errors;
using Talabat.Helper;
using Talabat.Repository;
using Talabat.Service;

namespace Talabat.Extensions
{
    public static class AddApplicationSerivce
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddSingleton<IResponceCaching, CachResponse>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<IOrderService,OrderService>();
            services.AddScoped(typeof(ITokenService), typeof(TokenService));
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MappingProfile));
            services.Configure<ApiBehaviorOptions>
                (option => option.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var error = actionContext.ModelState.Where(m => m.Value.Errors.Count() > 0)
                    .SelectMany(m => m.Value.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToArray();
                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = error
                    };
                    return new BadRequestObjectResult(validationErrorResponse);
                });
            return services;
        }

    }
}
