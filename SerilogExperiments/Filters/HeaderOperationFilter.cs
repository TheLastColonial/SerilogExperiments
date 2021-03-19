namespace SerilogExperiments.Filters
{
    using System.Collections.Generic;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class HeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(
                new OpenApiParameter
                {
                    Name = Constants.HttpHeaderConstants.CorrelationId,
                    In = ParameterLocation.Header,
                    Description = "An ID that can be used to trace the request across multiple application boundaries",
                    Required = true,
                    // Example = OpenApiAnyFactory.CreateFromJson("{\"ValueKind\"}") // Todo Fix auto generated example
                    
                });

            operation.Parameters.Add(
                new OpenApiParameter
                {
                    Name = Constants.HttpHeaderConstants.MerchantId,
                    In = ParameterLocation.Header,
                    Description = "Reference to the consuming Merchant",
                    Required = true
                });
        }
    }
}
