using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApp.Middlewares
{
    public sealed class SecurityMiddleware
    {
        private readonly RequestDelegate next;

        public SecurityMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("Content-Security-Policy", SetContentSecurityPolicy());
            context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-Frame-Options", "DENY");
            context.Response.Headers.Add("Referrer-Policy", "no-referrer");
            context.Response.Headers.Add("Feature-Policy", SetFeaturePolicy());
            context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");

            await next.Invoke(context);
        }

        private static string SetContentSecurityPolicy()
        {
            return "default-src 'self'; " +
                   "script-src 'self';" +
                   "style-src 'self'";
        }

        private static string SetFeaturePolicy()
        {
            return "camera 'none'; " +
                   "accelerometer 'none'; " +
                   "geolocation 'none'; " +
                   "magnetometer 'none'; " +
                   "microphone 'none'; " +
                   "usb 'none'";
        }
    }
}