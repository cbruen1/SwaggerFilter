using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SwaggerFilterTest.SwaggerFilters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerFilterTest
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
        /// </summary>
        /// <param name="options">The options specified by <see cref="SwaggerGenOptions"/> object</param>
        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            // Filter out api-version parameters globally
            options.OperationFilter<ApiVersionFilter>();

            // Create Swagger documents per version and consumer
            options.SwaggerDoc(Constants.ApiVersion1, CreateInfoForApiVersion("v1.0", "My Account API V1"));
            options.SwaggerDoc(Constants.ApiConsumerGroupNameConA, CreateInfoForApiVersion("v2.0", $"My Account API V2 {Constants.ApiConsumerNameConA}"));
            options.SwaggerDoc(Constants.ApiConsumerGroupNameConB, CreateInfoForApiVersion("v2.0", $"My Account API V2 {Constants.ApiConsumerNameConB}"));
            options.SwaggerDoc(Constants.ApiConsumerGroupNameConC, CreateInfoForApiVersion("v2.0", $"My Account API V2 {Constants.ApiConsumerNameConC}"));

            // Include all paths
            options.DocInclusionPredicate((name, api) => true);

            // Filter endpoints based on consumer
            options.DocumentFilter<SwaggerDocumentFilter>();

            // Take first description on any conflict
            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        }

        static OpenApiInfo CreateInfoForApiVersion(string version, string title)
        {
            var info = new OpenApiInfo()
            {
                Title = title,
                Version = version
            };

            return info;
        }
    }
}
