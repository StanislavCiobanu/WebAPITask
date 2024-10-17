using WebAPITask.TimerService;

namespace WebAPITask.Middleware
{
    public class TimeServiceMiddleware
    {
        private readonly ILogger<TimeServiceMiddleware> _logger;
        private readonly RequestDelegate _next;
        private readonly ITimerService _timerService;

        public TimeServiceMiddleware(RequestDelegate next, ILogger<TimeServiceMiddleware> logger, ITimerService timerService) 
        {
            _next = next;
            _logger = logger;   
            _timerService = timerService;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            _logger.LogInformation($"Request : {context.Request.Path} : {_timerService.GetCurrentTime().ToString()}");

            await _next(context);

            _logger.LogInformation($"Response : {context.Request.Path} : {_timerService.GetCurrentTime().ToString()}");
        }
    }

    public static class TimeServiceMiddlewareExtentions
    {
        public static IApplicationBuilder UseTimerServiceMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TimeServiceMiddleware>();
        }
    }
}
