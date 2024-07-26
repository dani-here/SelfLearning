namespace SelfLearning.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly string _actionType;

        public LoggingMiddleware(ILogger<LoggingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
            _actionType = $"{AppDomain.CurrentDomain.FriendlyName} Service Access";
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("{0} Request Received: {1} {2}", [_actionType, context.Request.Method, context.Request.Path]);
            await _next(context);
            _logger.LogInformation("{0} Response Generated: {1}", [_actionType, context.Response.StatusCode]);
        }
    }
}
