namespace SerilogExperiments.Filters
{
    using System;
    using System.Collections.Generic;
    using Microsoft.OpenApi.Any;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class HeaderOperationFilter : IOperationFilter
    {
        private const string merchantIdExample = "/merchant/12345";

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
                    Schema = new OpenApiSchema()
                    {
                        //Type = "String",
                        // Format = "guid", // https://swagger.io/docs/specification/data-models/data-types/
                        Default = new OpenApiString(Guid.NewGuid().ToString())
                    }                    
                });

            operation.Parameters.Add(
                new OpenApiParameter
                {
                    Name = Constants.HttpHeaderConstants.MerchantId,
                    In = ParameterLocation.Header,
                    Description = "Reference to the consuming Merchant",
                    Required = true,
                    Schema = new OpenApiSchema()
                    {
                        //Type = "String",
                        // Format = "uri",
                        Default = new OpenApiString(merchantIdExample)
                    }
                });
        }
    }
}
