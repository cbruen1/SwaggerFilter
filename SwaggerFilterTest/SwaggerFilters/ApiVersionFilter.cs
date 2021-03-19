using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerFilterTest.SwaggerFilters
{
    public class ApiVersionFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Remove version parameter field from Swagger UI
            var parametersToRemove = operation.Parameters.Where(x => x.Name == "api-version").ToList();
            foreach (var parameter in parametersToRemove)
            {
                operation.Parameters.Remove(parameter);
            }
        }
    }
}
