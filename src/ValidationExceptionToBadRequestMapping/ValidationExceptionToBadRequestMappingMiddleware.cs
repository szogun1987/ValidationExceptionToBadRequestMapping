using System.IO;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ValidationExceptionToBadRequestMapping
{
    internal class ValidationExceptionToBadRequestMappingMiddleware
    {
        private readonly RequestDelegate _next;
        private HttpStatusCode _failureCode;

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
                var jsonSerializer = JsonSerializer.CreateDefault(new JsonSerializerSettings{ContractResolver = new CamelCasePropertyNamesContractResolver()});
                using (var streamWriter = new StreamWriter(httpContext.Response.Body))
                {
                    httpContext.Response.StatusCode = (int) _failureCode;
                    var jsonWriter = new JsonTextWriter(streamWriter);

                    jsonWriter.CloseOutput = false;
                    jsonSerializer.Serialize(jsonWriter, e.Errors);

                    await streamWriter.FlushAsync();
                }
            }
        }
    }
}
