using EthereumTransactionSearch.Middleware;
using EthereumTransactionSearch.Services;
using EthereumTransactionSearch.Services.HealthChecks;
using EthereumTransactionSearch.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;
using Serilog;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;

namespace EthereumTransactionSearch
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

			services.AddHealthChecks()
				.AddCheck<SystemHealthCheck>("system_health_check");

			services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Ethereum Transaction Search API",
                        Version = "v1"
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

			services.AddHttpClient<ITransactionSearchService, InfuraTransactionSearchService>()
				.SetHandlerLifetime(TimeSpan.FromMinutes(5))
				.AddPolicyHandler(GetRetryPolicy()); ;
				
			services.AddTransient<ITransactionSearchService, InfuraTransactionSearchService>();

			var infuraSettings = Configuration.GetSection("Infura");
			services.Configure<InfuraSettings>(infuraSettings);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHealthChecks("/health", new HealthCheckOptions()
				{
					ResultStatusCodes =
					{
						[HealthStatus.Healthy] = StatusCodes.Status200OK,
						[HealthStatus.Degraded] = StatusCodes.Status200OK,
						[HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
					}
				});
			});

			app.UseSerilogRequestLogging();

			app.UseMiddleware<LogContextMiddleware>();

			app.UseAuthorization();

			app.UseCors(
				options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
			);

			app.UseSwagger();
			app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ethereum Transaction Search V1"); });

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
		{
			return HttpPolicyExtensions
				.HandleTransientHttpError()
				.OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
				.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
																			retryAttempt)));
		}
	}
}
