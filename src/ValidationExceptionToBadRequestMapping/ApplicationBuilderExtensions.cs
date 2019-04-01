﻿using Microsoft.AspNetCore.Builder;

namespace ValidationExceptionToBadRequestMapping
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseValidationExceptionToBadRequestMapping(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidationExceptionToBadRequestMappingMiddleware>();
        }
    }
}