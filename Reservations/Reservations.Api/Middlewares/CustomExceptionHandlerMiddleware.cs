using Reservations.Application.Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Reservations.Api.Middlewares
{
    /// <summary>
    /// Custom Exception Handling Middleware
    /// </summary>
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="next"></param>
        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke middleware
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, ILogger<CustomExceptionHandlerMiddleware> logger, IHostEnvironment environment)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, logger, environment);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<CustomExceptionHandlerMiddleware> logger, IHostEnvironment environment)
        {
            var code = HttpStatusCode.InternalServerError;

            var result = string.Empty;

            switch (exception)
            {
                //case ValidationException validationException:
                //    code = HttpStatusCode.BadRequest;
                //    result = JsonConvert.SerializeObject(new { errors = validationException.Failures });
                //    break;
                case BadRequestException _:
                    code = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(new { error = exception.Message });
                    break;
                case NotFoundException _:
                    result = JsonConvert.SerializeObject(new { error = exception.Message });
                    code = HttpStatusCode.NotFound;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            // return full exception in case when environment is set to Development
            if ((environment.IsDevelopment() || environment.IsEnvironment("Local")) && code == HttpStatusCode.InternalServerError)
            {
                result = JsonConvert.SerializeObject(new { error = new { message = exception.Message, stackTrace = exception.StackTrace } });
            }

            if (code == HttpStatusCode.InternalServerError)
            {
                logger.LogError(exception, "Unexpected error occured while processing request.");
            }
            else
            {
                logger.LogDebug("Business exception raised. Error message: {Result}", result);
            }

            return context.Response.WriteAsync(result);
        }
    }

    internal static class CustomExceptionHandlerMiddlewareExtensions
    {
        internal static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
