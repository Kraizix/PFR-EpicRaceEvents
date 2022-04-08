namespace App.Middlewares
{
    public class BasicMiddleware
    {
        private readonly RequestDelegate _next;

        public BasicMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("...MW1 ===>");
            await _next(context);
            Console.WriteLine("<=== MW1 ...");
        }
    }
}