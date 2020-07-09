using EthereumTransactionSearch.Dtos.Infura;
using EthereumTransactionSearch.Helpers;
using EthereumTransactionSearch.Models;
using EthereumTransactionSearch.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nethereum.Util;
using Newtonsoft.Json;
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

			_logger.LogInformation($"Sending request to Infura");

			var response = await httpClient.SendAsync(httpRequest);

			_logger.LogInformation($"Received request from Infura");

			var strContent = await response.Content.ReadAsStringAsync();

			InfuraTransactionSearchResponse infuraTransactionSearchResponse = JsonConvert.DeserializeObject<InfuraTransactionSearchResponse>(strContent);
			
			var transactions = infuraTransactionSearchResponse.Result.Transactions.Where(m => (m.From == transactionSearchRequest.Address || m.To == transactionSearchRequest.Address)).ToList();

			TransactionSearchResponse transactionSearchResponse = new TransactionSearchResponse
			{ 
				Transactions = transactions.Select(m => new TransactionResults 
				{
					BlockHash = m.BlockHash,
					BlockNumber = ValueConverter.HexToLong(m.BlockNumber),
					From = m.From,
					To = m.To,
					Gas = UnitConversion.Convert.FromWei(ValueConverter.HexToBigInt(m.Gas), UnitConversion.EthUnit.Ether),
					Hash = m.Hash,
					Value = UnitConversion.Convert.FromWei(ValueConverter.HexToBigInt(m.Value), UnitConversion.EthUnit.Ether),
					TransactionIndex = ValueConverter.HexToLong(m.TransactionIndex)
				}).ToList()
			};

			_logger.LogInformation($"Searching transactions completed");

			return transactionSearchResponse;
		}
	}
}
