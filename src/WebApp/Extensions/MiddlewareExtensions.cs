using Microsoft.AspNetCore.Builder;
using WebApp.Middlewares;

namespace WebApp.Extensions
{
    internal static class MiddlewareExtensions
    {
        internal static IApplicationBuilder UseSecurityMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SecurityMiddleware>();
        }
    }
}