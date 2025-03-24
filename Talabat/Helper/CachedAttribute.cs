using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Services;

namespace Talabat.Helper
{
    public class CachedAttribute : Attribute, IAsyncActionFilter


    {
        private readonly int _timeLive;

        public CachedAttribute(int timeLive)
        {
            _timeLive = timeLive;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cachingServer = context.HttpContext.RequestServices.GetRequiredService<IResponceCaching>();
            var cachKey = GeneratCachKeyFromRequest(context.HttpContext.Request);
            var cachedResponse = await cachingServer.GetCachingResponseAsync(cachKey);
            if (!String.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                 context.Result = contentResult;
                return;

            }
            var executedEndPointContext = await next();
            if (executedEndPointContext.Result is OkObjectResult okObjectResult)
            {
                await cachingServer.CachingResponseAsync(cachKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeLive));
            }
        }

        private string GeneratCachKeyFromRequest(HttpRequest request)
        {
            var keyBuilder =new StringBuilder();
            keyBuilder.Append(request.Path);
            foreach (var (key, value) in request.Query.OrderBy(x=>x.Key)) 
            { 
                keyBuilder.Append($"|{key}-{value}");
            }
            return keyBuilder.ToString();
        }
    }
}
