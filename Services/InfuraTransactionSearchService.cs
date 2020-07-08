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
				_logger.LogInformation($"Searching blockNumber: {transactionSearchRequest.BlockNumber} and accountHash: {transactionSearchRequest.Address}");

				HttpClient httpClient = _httpClientFactory.CreateClient();

				string blockNumberInHex = $"0x{transactionSearchRequest.BlockNumber.ToString("X")}";

				InfuraTransactionSearchRequest infuraTransactionSearchRequest = new InfuraTransactionSearchRequest
				{
					Jsonrpc = "2.0",
					Method = "eth_getBlockByNumber",
					Params = new object[] { blockNumberInHex, true},
					Id = 1
				};

				var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{_infuraSettings.BaseUrl}/{_infuraSettings.ProjectId}")
				{
					Content = new StringContent(JsonConvert.SerializeObject(infuraTransactionSearchRequest), Encoding.UTF8, "application/json")
				};

				var response = await httpClient.SendAsync(httpRequest);
				var strContent = await response.Content.ReadAsStringAsync();

				InfuraTransactionSearchResponse infuraTransactionSearchResponse = JsonConvert.DeserializeObject<InfuraTransactionSearchResponse>(strContent);

				//TODO filter out transactions by account hash and get ether value from WEI

				//TODO map to transaction search response

			}
			catch (Exception ex)
			{
				_logger.LogError("Exception caught!", ex);
			}

			return null;
		}
	}
}
