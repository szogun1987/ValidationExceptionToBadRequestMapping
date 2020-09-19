using Microsoft.AspNetCore.Builder;
using System.Net;

namespace ValidationExceptionToBadRequestMapping
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseValidationExceptionToBadRequestMapping(this IApplicationBuilder builder, HttpStatusCode failureCode = HttpStatusCode.BadRequest)
        {
            return builder.UseMiddleware<ValidationExceptionToBadRequestMappingMiddleware>(failureCode);
        }
    }
}