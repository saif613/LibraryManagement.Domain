namespace LibraryManagement.Presentation.Middleware
{
    public sealed class RequestLoggingMiddleware: IMiddleware
    {
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _logger.LogInformation("Request: {Method} {Path}", context.Request.Method, context.Request.Path);

            await next(context);

            _logger.LogInformation("Response: {StatusCode}", context.Response.StatusCode);
        }
    }
}
