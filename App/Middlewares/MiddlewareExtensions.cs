namespace App.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseBasicMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<BasicMiddleware>();
        }
    }
}