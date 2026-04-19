namespace LibraryManagement.Presentation.Middleware
{
    public sealed class RequestLoggingMiddleware : IMiddleware
    {
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var request = context.Request;

            // start time
            var startTime = DateTime.UtcNow;

            // Correlation ID (مهم جدًا للتتبع)
            var correlationId = Guid.NewGuid().ToString();

            _logger.LogInformation(
                "Request [{CorrelationId}]: {Method} {Path} started",
                correlationId,
                request.Method,
                request.Path
            );

            try
            {
                await next(context);
            }
            finally
            {
                var duration = DateTime.UtcNow - startTime;

                _logger.LogInformation(
                    "Response [{CorrelationId}]: {StatusCode} completed in {Duration} ms",
                    correlationId,
                    context.Response.StatusCode,
                    duration.TotalMilliseconds
                );
            }
        }
    }
}