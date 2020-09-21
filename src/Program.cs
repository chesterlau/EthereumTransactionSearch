using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System.IO;

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
						.UseUrls("http://*:5000")
						.UseContentRoot(Directory.GetCurrentDirectory())
						.ConfigureAppConfiguration((hostingContext, config) =>
						{
							var azureServiceTokenProvider = new AzureServiceTokenProvider();
							var keyVaultClient = new KeyVaultClient(
							  new KeyVaultClient.AuthenticationCallback(
								 azureServiceTokenProvider.KeyVaultTokenCallback));

							var env = hostingContext.HostingEnvironment;
							config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
								  .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
									  optional: true, reloadOnChange: true)
									  .AddAzureKeyVault("https://ethtransactionkv.vault.azure.net/", keyVaultClient, new DefaultKeyVaultSecretManager());
						})
						.UseSerilog((ctx, config) =>
						{
							config.ReadFrom.Configuration(ctx.Configuration);
							config.MinimumLevel.Override("Microsoft", LogEventLevel.Information);
							config.MinimumLevel.Override("System", LogEventLevel.Warning);
						}).UseStartup<Startup>();
				});
	}
}
