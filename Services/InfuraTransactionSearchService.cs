using EthereumTransactionSearch.Dtos.Infura;
using EthereumTransactionSearch.Models;
using EthereumTransactionSearch.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EthereumTransactionSearch.Services
{
	public class InfuraTransactionSearchService : ITransactionSearchService
	{
		private readonly InfuraSettings _infuraSettings;
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ILogger<InfuraTransactionSearchService> _logger;

		public InfuraTransactionSearchService(IOptions<InfuraSettings> infuraSettings, IHttpClientFactory httpClientFactory, ILogger<InfuraTransactionSearchService> logger)
		{
			_httpClientFactory = httpClientFactory;
			_infuraSettings = infuraSettings.Value;
			_logger = logger;
		}

		public async Task<TransactionSearchResponse> Search(TransactionSearchRequest transactionSearchRequest)
		{
			try
			{
				//TODO log request

				HttpClient httpClient = _httpClientFactory.CreateClient();

				//TODO convert block number to hex

				InfuraTransactionSearchRequest infuraTransactionSearchRequest = new InfuraTransactionSearchRequest
				{
					jsonrpc = "2.0",
					method = "eth_getBlockByNumber",
					@params = new object[] { "0x8B99C9", true},
					id = 1
				};

				var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{_infuraSettings.BaseUrl}/{_infuraSettings.ProjectId}")
				{
					Content = new StringContent(JsonConvert.SerializeObject(infuraTransactionSearchRequest), Encoding.UTF8, "application/json")
				};

				var response = await httpClient.SendAsync(httpRequest);
				var strContent = await response.Content.ReadAsStringAsync();

				//TODO deserialise object

				//TODO filter out transactions by account hash

			}
			catch (Exception ex)
			{
				_logger.LogError("Exception caught!", ex);
			}

			return null;
		}
	}
}
