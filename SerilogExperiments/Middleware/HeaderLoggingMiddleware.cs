namespace SerilogExperiments.Middleware
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Serilog.Context;
    using SerilogExperiments.Constants;
    using SerilogExperiments.Services;

    /// <summary>
    /// Logging http header information for context
    /// </summary>
    /// <remarks>
    /// By using <see cref="LogContext"/> the properties are persisted though all logging not just the top level.
    /// However not sure saving the value to a logical thread will cause issues at scale.
    /// </remarks>
    /// <seealso cref="https://nblumhardt.com/2020/09/serilog-inject-dependencies/"/>
    public class HeaderLoggingMiddleware
    {
        private readonly RequestDelegate next;

        public HeaderLoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IHttpHeaderAccessor httpHeaderAccessor)
        {
            using (LogContext.PushProperty(HttpHeaderConstants.CorrelationId, httpHeaderAccessor.CorrelationId))
            using (LogContext.PushProperty(HttpHeaderConstants.MerchantId, httpHeaderAccessor.MerchantId))
            {
                await this.next(httpContext);
            }
        }
    }
}
