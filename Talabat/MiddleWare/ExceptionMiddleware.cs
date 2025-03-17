using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Errors;

namespace Talabat.MiddleWare
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> Logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next ,ILogger<ExceptionMiddleware> Logger,IHostEnvironment env)
        {
            this.next = next;
            this.Logger = Logger;
            this.env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await next.Invoke(context);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex,ex.Message);
                //log Exception at Database
                context.Response.ContentType = "application/json";
                context.Response.StatusCode =(int) HttpStatusCode.InternalServerError;
                var exceptionErrorResponse=env.IsDevelopment()?
                    new ApiExceptionResponse(500,ex.Message,ex.StackTrace.ToString()) 
                    :
                    new ApiExceptionResponse(500);
                var option= new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json=JsonSerializer.Serialize(exceptionErrorResponse,option);
                await context.Response.WriteAsync(json); 
            }  
        }
    }
}
