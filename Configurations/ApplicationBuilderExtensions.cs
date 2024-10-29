﻿namespace GlobalExceptionHandling.Configurations;

public static class ApplicationBuilderExtensions
{

    public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
}

