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
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SwaggerFilterTest
{
	public class Startup
	{
		private static Startup Instance { get; set; }

		private static string AssemblyName { get; }

		private static string FullVersionNo { get; }

		private static string MajorMinorVersionNo { get; }

		static Startup()
		{
			var fmt = CultureInfo.InvariantCulture;
			var assemblyName = Assembly.GetExecutingAssembly().GetName();
			AssemblyName = assemblyName.Name;
			FullVersionNo = string.Format(fmt, "v{0}", assemblyName.Version.ToString());
			MajorMinorVersionNo = string.Format(fmt, "v{0}.{1}",
				assemblyName.Version.Major, assemblyName.Version.Minor);
		}

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			Instance = this;
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

			// Use an IConfigureOptions for the settings
			services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

			services.AddSwaggerGen(c =>
			{
				c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

				// Group by tag
				c.EnableAnnotations();

				// Include comments for current assembly
				var xmlFile = $"{AssemblyName}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
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

				// Build a swagger endpoint for each API version and consumer
				c.SwaggerEndpoint($"/swagger/{Constants.ApiVersion1}/swagger.json", "MyAccount API V1");
				c.SwaggerEndpoint($"/swagger/{Constants.ApiConsumerGroupNameConA}/swagger.json", $"MyAccount API V2 {Constants.ApiConsumerNameConA}");
				c.SwaggerEndpoint($"/swagger/{Constants.ApiConsumerGroupNameConB}/swagger.json", $"MyAccount API V2 {Constants.ApiConsumerNameConB}");
				c.SwaggerEndpoint($"/swagger/{Constants.ApiConsumerGroupNameConC}/swagger.json", $"MyAccount API V2 {Constants.ApiConsumerNameConC}");

				c.DocExpansion(DocExpansion.List);
			});
		}
	}
}
