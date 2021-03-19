namespace SerilogExperiments.Middleware
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using Serilog.Context;
    using SerilogExperiments.Constants;

    /// <summary>
    /// Logging http header information for context
    /// </summary>
    /// <remarks>
    /// By using <see cref="LogContext"/> the properties are persisted though all logging not just the top level.
    /// However not sure saving the value to a logical thread will cause issues at scale.
    /// </remarks>
    public class HeaderLoggingMiddlewareV2
    {
        private readonly RequestDelegate next;

        public HeaderLoggingMiddlewareV2(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            using (LogContext.PushProperty(HttpHeaderConstants.CorrelationId, this.GetCorrelationId(httpContext)))
            using (LogContext.PushProperty(HttpHeaderConstants.MerchantId, this.GetMerchantId(httpContext)))
            {
                await this.next(httpContext);
            }
        }

        protected StringValues GetCorrelationId(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.TryGetValue(HttpHeaderConstants.CorrelationId, out var correlationId))
            {
                return correlationId;
            }

            return new StringValues("Unknown");
        }

        protected StringValues GetMerchantId(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.TryGetValue(HttpHeaderConstants.MerchantId, out var merchantId))
            {
                return merchantId;
            }

            return new StringValues("Unknown");            
        }
    }
}
