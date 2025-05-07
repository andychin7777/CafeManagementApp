using DomainResults.Common;
using System.Net;
using System.Text.Json;

namespace CafeManagementApp.Server.Infrastructure.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger">ioc injected into the invoke function</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var message = error.Message;

                switch (error)
                {
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                //set the naming policy to null to default to default casing.
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = null
                };

                await response.WriteAsJsonAsync(DomainResult.Failed(message), jsonOptions);
            }
        }
    }
}
