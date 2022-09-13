using FinanceTracker.Web.Middleware;

namespace FinanceTracker.Web.Extensions
{
    public static class DbExtensions
    {
        public static IApplicationBuilder UseDbInitializer(this IApplicationBuilder app)
        {
            return app.UseMiddleware<DbInitializeMiddleware>();
        }
    }
}
