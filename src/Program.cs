using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace EthereumTransactionSearch
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseKestrel()
						//.UseUrls("http://*:5000")
						.UseContentRoot(Directory.GetCurrentDirectory())
						.ConfigureAppConfiguration((hostingContext, config) =>
						{
							var env = hostingContext.HostingEnvironment;
							config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
								  .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
									  optional: true, reloadOnChange: true);
							config.AddEnvironmentVariables();
						})
						.UseSerilog((ctx, config) =>
						{
							config.ReadFrom.Configuration(ctx.Configuration);
							config.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
							config.MinimumLevel.Override("System", LogEventLevel.Warning);
						}).UseStartup<Startup>();
				});
	}
}
