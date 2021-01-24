using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerFilterTest
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

			services.AddApiVersioning(options =>
			{
				options.ReportApiVersions = true;
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.DefaultApiVersion = new ApiVersion(1, 0);
			});

			services.AddVersionedApiExplorer(options =>
			{
				options.GroupNameFormat = "'v'VV";
				options.SubstituteApiVersionInUrl = true;
			});

			services.AddSwaggerGen(c =>
			{
				c.OperationFilter<ApiVersionFilter>();

				c.SwaggerDoc("v1", new OpenApiInfo() { Title = "My API - Version 1", Version = "v1.0" });
				c.SwaggerDoc("v2-conA", new OpenApiInfo() { Title = "My API - Version 2", Version = "v2.0" });
				c.SwaggerDoc("v2-conB", new OpenApiInfo() { Title = "My API - Version 2", Version = "v2.0" });
				c.SwaggerDoc("v2-conC", new OpenApiInfo() { Title = "My API - Version 2", Version = "v2.0" });

				c.TagActionsBy(api =>
				{
					if (api.GroupName != null)
					{
						return new[] { api.GroupName };
					}

					var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
					if (controllerActionDescriptor != null)
					{
						return new[] { controllerActionDescriptor.ControllerName };
					}

					return null;
				});

				c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
				c.EnableAnnotations();
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.EnableDeepLinking();
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
				c.SwaggerEndpoint("/swagger/v2-conA/swagger.json", "My API V2 ConA");
				c.SwaggerEndpoint("/swagger/v2-conB/swagger.json", "My API V2 ConB");
				c.SwaggerEndpoint("/swagger/v2-conC/swagger.json", "My API V2 ConC");
			});
		}
	}

	internal class ApiVersionFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			var paramsToRemove = operation.Parameters.Where(p => p.Name == "api-version").ToList();
			foreach (var item in paramsToRemove)
			{
				operation.Parameters.Remove(item);
			}
		}
	}
}
