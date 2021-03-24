namespace SerilogExperiments.Services
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using SerilogExperiments.Constants;

    public interface IHttpHeaderAccessor
    {
        Guid CorrelationId { get; }
        string MerchantId { get; }
    }

    public class HttpHeaderAccessor : IHttpHeaderAccessor
    {
        private readonly IHttpContextAccessor contextAccessor;

        public HttpHeaderAccessor(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public Guid CorrelationId => this.GetCorrelationId(this.contextAccessor.HttpContext);

        public string MerchantId => this.GetMerchantId(this.contextAccessor.HttpContext).ToString();

        protected Guid GetCorrelationId(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.TryGetValue(HttpHeaderConstants.CorrelationId, out var idText))
            {
                if (Guid.TryParse(idText, out Guid correlationId))
                {
                    return correlationId;
                }

                throw new NotImplementedException("Gained header information but not parsed successfully");
            }

            return Guid.Empty;
        }

        protected string GetMerchantId(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.TryGetValue(HttpHeaderConstants.MerchantId, out var merchantId))
            {
                return merchantId.ToString();
            }

            return new StringValues("Unknown").ToString();
        }
    }
}
