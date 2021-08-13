using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;


namespace ValidationExceptionToBadRequestMapping
{
    internal class ValidationExceptionToBadRequestMappingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpStatusCode _failureCode;

        public ValidationExceptionToBadRequestMappingMiddleware(RequestDelegate next, HttpStatusCode failureCode)
        {
            _next = next;
            _failureCode = failureCode;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException e)
            {
                
                await using var streamWriter = new StreamWriter(httpContext.Response.Body);

                httpContext.Response.StatusCode = (int) _failureCode;
                
                await JsonSerializer.SerializeAsync(httpContext.Response.Body, e.Errors).ConfigureAwait(false);

                await streamWriter.FlushAsync().ConfigureAwait(false);
            }
        }
    }
}
