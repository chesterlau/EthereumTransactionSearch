using EthereumTransactionSearch.Dtos.Infura;
using EthereumTransactionSearch.Helpers;
using EthereumTransactionSearch.Models;
using EthereumTransactionSearch.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nethereum.Util;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static EthereumTransactionSearch.Models.Enumerations;

namespace EthereumTransactionSearch.Services
{
	public class InfuraTransactionSearchService : ITransactionSearchService
	{
		private const string JSONRPC = "2.0";
		private readonly InfuraSettings _infuraSettings;
		private readonly IConfiguration _configuration;
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ILogger<InfuraTransactionSearchService> _logger;

		public InfuraTransactionSearchService(IConfiguration configuration, IOptions<InfuraSettings> infuraSettings, IHttpClientFactory httpClientFactory, ILogger<InfuraTransactionSearchService> logger)
		{
			_httpClientFactory = httpClientFactory;
			_configuration = configuration;
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
				Jsonrpc = JSONRPC,
				Method = InfuraMethod.eth_getBlockByNumber.ToString(),
				Params = new object[] { blockNumberInHex, true},
				Id = 1
			};

			_logger.LogInformation($"The infura token from config is: {_configuration["InfuraToken"]}");
			_logger.LogInformation($"The token from infura settings is {_infuraSettings.Token}");

			var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{_infuraSettings.BaseUrl}/{_infuraSettings.Token}")
			{
				Content = new StringContent(JsonConvert.SerializeObject(infuraTransactionSearchRequest), Encoding.UTF8, "application/json")
			};

			_logger.LogInformation($"Sending request to Infura");

			var response = await httpClient.SendAsync(httpRequest);

			_logger.LogInformation($"Received request from Infura");

			var strContent = await response.Content.ReadAsStringAsync();

			InfuraTransactionSearchResponse infuraTransactionSearchResponse = JsonConvert.DeserializeObject<InfuraTransactionSearchResponse>(strContent);
			
			var address = transactionSearchRequest.Address.Replace(" ", string.Empty);

			var transactions = infuraTransactionSearchResponse.Result.Transactions.Where(m => (string.Equals(m.From, address, System.StringComparison.OrdinalIgnoreCase) 
			|| string.Equals(m.To, address, System.StringComparison.OrdinalIgnoreCase))).ToList();

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
