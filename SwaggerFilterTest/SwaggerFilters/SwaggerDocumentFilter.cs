using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerFilterTest.SwaggerFilters
{
    public class SwaggerDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// Applies the document filtering
        /// </summary>
        /// <param name="swaggerDoc">The <see cref="OpenApiDocument"/> Swagger document</param>
        /// <param name="context">The <see cref="DocumentFilterContext"/> object</param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // Key is read-only so make a copy of the Paths property
            var pathsPerConsumer = new OpenApiPaths();
            var currentConsumer = GetConsumer(swaggerDoc.Info.Title);
            IDictionary<string, OpenApiSchema> allSchemas = swaggerDoc.Components.Schemas;

            if (swaggerDoc.Info.Version.Contains(Constants.ApiVersion2))
            {
                foreach (var path in swaggerDoc.Paths)
                {
                    // If there are any tags (all methods are decorated with "SwaggerOperation(Tags = new[]...") with the current consumer name
                    if (path.Value.Operations.Values.FirstOrDefault().Tags
                        .Where(t => t.Name.Contains(currentConsumer)).Any())
                    {
                        // Remove tags not applicable to the current consumer (for endpoints where multiple consumers have access)
                        var newPath = RemoveTags(currentConsumer, path);

                        // Add the path to the collection of paths for current consumer
                        pathsPerConsumer.Add(newPath.Key, newPath.Value);
                    }
                }

                //// Whatever objects are used as parameters or return objects in the API will be listed under the Schemas section in the Swagger UI
                //// Use below to filter them based on the current consumer - remove schemas not belonging to the current path
                
                //foreach (KeyValuePair<string, OpenApiSchema> schema in allSchemas)
                //{
                //    // Get the schemas for current consumer
                //    if (Constants.ApiPathSchemas.TryGetValue(currentConsumer, out List<string> schemaList))
                //    {
                //        if (!schemaList.Contains(schema.Key))
                //        {
                //            swaggerDoc.Components.Schemas.Remove(schema.Key);
                //        }
                //    }
                //}
            }
            else
            {
                // For version 1 list version 1 endpoints only
                foreach (var path in swaggerDoc.Paths)
                {
                    if (!path.Key.Contains(Constants.ApiVersion2))
                    {
                        pathsPerConsumer.Add(path.Key, path.Value);
                    }
                }
            }

            swaggerDoc.Paths = pathsPerConsumer;
        }

        /// <summary>
        /// Remove tags not applicable to the current consumer
        /// </summary>
        /// <param name="currentConsumer">Current consumer</param>
        /// <param name="path">The current path</param>
        /// <returns>A Swagger path in the form of a key value pair</returns>
        public KeyValuePair<string, OpenApiPathItem> RemoveTags(string currentConsumer, KeyValuePair<string, OpenApiPathItem> path)
        {
            foreach (var item in path.Value.Operations.Values?.FirstOrDefault().Tags?.ToList())
            {
                // If the tag name doesn't contain the current consumer name remove it
                if (!item.Name.Contains(currentConsumer))
                {
                    path.Value.Operations.Values?.FirstOrDefault().Tags?.Remove(item);
                }
            }

            return path;
        }

        private string GetConsumer(string path)
        {
            if (path.Contains(Constants.ApiConsumerNameConA))
            {
                return Constants.ApiConsumerNameConA;
            }
            else if (path.Contains(Constants.ApiConsumerNameConB))
            {
                return Constants.ApiConsumerNameConB;
            }
            else if (path.Contains(Constants.ApiConsumerNameConC))
            {
                return Constants.ApiConsumerNameConC;
            }

            return string.Empty;
        }
    }
}