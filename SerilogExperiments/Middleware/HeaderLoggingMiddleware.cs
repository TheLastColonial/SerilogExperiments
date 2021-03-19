namespace SerilogExperiments.Middleware
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Serilog;
    using SerilogExperiments.Constants;

    public class HeaderLoggingMiddleware
    {
        private readonly RequestDelegate next;

        public HeaderLoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IDiagnosticContext diagnosticContext)
        {
            this.SetCorrelationId(httpContext, diagnosticContext);
            this.SetMerchantId(httpContext, diagnosticContext);
            // Timestamp
            await this.next(httpContext);
        }

        protected void SetCorrelationId(HttpContext httpContext, IDiagnosticContext diagnosticContext)
        {
            httpContext.Request.Headers.TryGetValue(HttpHeaderConstants.CorrelationId, out var correlationId);
            diagnosticContext.Set(HttpHeaderConstants.CorrelationId, correlationId);
        }

        protected void SetMerchantId(HttpContext httpContext, IDiagnosticContext diagnosticContext)
        {
            httpContext.Request.Headers.TryGetValue(HttpHeaderConstants.MerchantId, out var merchantId);
            diagnosticContext.Set(HttpHeaderConstants.MerchantId, merchantId);
        }
    }
}
