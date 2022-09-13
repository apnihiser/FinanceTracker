using FinanceTracker.DataAccess.DbInitializer;

namespace FinanceTracker.Web.Middleware
{
    public class DbInitializeMiddleware
    {
        private readonly RequestDelegate _next;

        public DbInitializeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IDbInitializer dbInit)
        {
            dbInit.Initalize();
            await _next.Invoke(context);
        }
    }
}
