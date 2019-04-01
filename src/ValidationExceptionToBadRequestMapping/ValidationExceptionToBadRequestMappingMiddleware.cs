using System.IO;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ValidationExceptionToBadRequestMapping
{
    internal class ValidationExceptionToBadRequestMappingMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionToBadRequestMappingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException e)
            {
                var jsonSerializer = JsonSerializer.CreateDefault();
                using (var streamWriter = new StreamWriter(httpContext.Response.Body))
                {
                    httpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    var jsonWriter = new JsonTextWriter(streamWriter);

                    jsonWriter.CloseOutput = false;
                    jsonSerializer.Serialize(jsonWriter, e.Errors);

                    await streamWriter.FlushAsync();
                }
            }
        }
    }
}
