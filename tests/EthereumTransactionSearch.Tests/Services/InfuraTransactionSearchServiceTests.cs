using EthereumTransactionSearch.Models;
using EthereumTransactionSearch.Services;
using EthereumTransactionSearch.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EthereumTransactionSearch.Tests.Services
{
	public class InfuraTransactionSearchServiceTests
	{
		//[Fact]
		//public async Task Search_Returns_Transactions_Based_On_Address_Correctly()
		//{
		//	//Arrange
		//	Mock<IOptions<InfuraSettings>> mockSettings = new Mock<IOptions<InfuraSettings>>();
		//	Mock<IHttpClientFactory> mockHttpClientFactory = new Mock<IHttpClientFactory>();
		//	Mock<ILogger<InfuraTransactionSearchService>> mockLogger = new Mock<ILogger<InfuraTransactionSearchService>>();

		//	string infuraSampleResponse = "{\r\n\"jsonrpc\":\"2.0\",\r\n\"id\":1,\r\n\"result\":{\r\n\"difficulty\":\"0x92acba0bc9ee9\",\r\n\"extraData\":\"0x5050594520737061726b706f6f6c2d6574682d636e2d687a34\",\r\n\"gasLimit\":\"0x9870b0\",\r\n\"gasUsed\":\"0x9861c4\",\r\n\"hash\":\"0x6dbde4b224013c46537231c548bd6ff8e2a2c927c435993d351866d505c523f1\",\r\n\"logsBloom\":\"0x26de480d43833fa8a58c064ea30993953c0a73009c3a5214150b1310a313632b644d002ffd84e95642a6dc7212b1c94790441e180d4a498b428cd28d34ecc660572405819c878da0e01cbe695a0ae88926c27eae9505239a201acb2588cd3d1366d749157bd5c8136d205011600b3f307f8162d5d4b24c365263073687e1700b0e08e31d56470048e7888c752220485b10765bc949ca384cfd1a119ac8b204609a4e2d4ecd0817dd0f89e788ecf9aaa4049260ce6780615ef8d280a066f480e52e90b0632998222db4d940c42104ad55fc3d12a0c60b218c0a9393e029c639b4b0b6543c10625408141ad4a49d803e8c0a44c90ab95183f7045a109849342847\",\r\n\"miner\":\"0x5a0b54d5dc17e0aadc383d2db43b0a0d3e029c4c\",\r\n\"mixHash\":\"0x15cb91e75a426171bc165baa20045d9012ed2c35f04c67998c647b438be93f33\",\r\n\"nonce\":\"0xe1709a0011208e95\",\r\n\"number\":\"0x8b99c9\",\r\n\"parentHash\":\"0x260e32bca150f18b32863cfe6490945376e7d3a9790e61e85c091d0d48c6cd81\",\r\n\"receiptsRoot\":\"0x42cf423349e2c5d9ad2ead1a98f3d571e426752f39f51edb197ea3e3d64aba67\",\r\n\"sha3Uncles\":\"0x1dcc4de8dec75d7aab85b567b6ccd41ad312451b948a7413f0a142fd40d49347\",\r\n\"size\":\"0x98d4\",\r\n\"stateRoot\":\"0xd330eff4f82be0732f867ec1d1f630d254a2aa42be23c40d0c0b10942e3c781f\",\r\n\"timestamp\":\"0x5e003833\",\r\n\"totalDifficulty\":\"0x2d6401524274158d1b9\",\r\n\"transactions\":[\r\n{\r\n\"blockHash\":\"0x6dbde4b224013c46537231c548bd6ff8e2a2c927c435993d351866d505c523f1\",\r\n\"blockNumber\":\"0x8b99c9\",\r\n\"from\":\"0xc779a4bdc3696baf2a6d62ddfc2d0664d3c4fd7f\",\r\n\"gas\":\"0x5208\",\r\n\"gasPrice\":\"0x12a05f2000\",\r\n\"hash\":\"0x827c9a4a1ae2cf9c20fa1dad305b4b8f8f336aab52ff9563379a8a9dbf0d1727\",\r\n\"input\":\"0x\",\r\n\"nonce\":\"0x9a\",\r\n\"r\":\"0x6055ae2794fad915c84b8ad5c60cc09522609c509f05177d8a5f65fd9025ab0e\",\r\n\"s\":\"0x334f79392ec759ea4d4baa7bbd55cd3f37900c7ba2ed10a56de7b2c32d01feea\",\r\n\"to\":\"0x92d44b6b3a23b48a93b1bce4d206c0280f96b1cd\",\r\n\"transactionIndex\":\"0x0\",\r\n\"v\":\"0x25\",\r\n\"value\":\"0x4115f164490000\"\r\n},\r\n{\r\n\"blockHash\":\"0x6dbde4b224013c46537231c548bd6ff8e2a2c927c435993d351866d505c523f1\",\r\n\"blockNumber\":\"0x8b99c9\",\r\n\"from\":\"0xc55eddadeeb47fcde0b3b6f25bd47d745ba7e7fa\",\r\n\"gas\":\"0x5208\",\r\n\"gasPrice\":\"0x4a817c800\",\r\n\"hash\":\"0x1fd509bc8a1f26351400f4ca206dbe7b8ebb8dcf3925ddf7201e8764e1bd3ea3\",\r\n\"input\":\"0x\",\r\n\"nonce\":\"0x95e7\",\r\n\"r\":\"0x8e0bd4787e3396dc1697ef278960f9d9743323d3e2b8d6a67f773f305385fe89\",\r\n\"s\":\"0x7fe255386e1bb617c630a0ff8afdb2cc1affab7367df48232bfaddb7bd5b9d22\",\r\n\"to\":\"0x59422595656a6b7c8917625607934d0e11fa0c40\",\r\n\"transactionIndex\":\"0x3e\",\r\n\"v\":\"0x1c\",\r\n\"value\":\"0x4563918244f400000\"\r\n},\r\n{\r\n\"blockHash\":\"0x6dbde4b224013c46537231c548bd6ff8e2a2c927c435993d351866d505c523f1\",\r\n\"blockNumber\":\"0x8b99c9\",\r\n\"from\":\"0xc55eddadeeb47fcde0b3b6f25bd47d745ba7e7fa\",\r\n\"gas\":\"0x5208\",\r\n\"gasPrice\":\"0x4a817c800\",\r\n\"hash\":\"0xfcbbca93ff416601e5be95838fcfa2c534c48027b10172c12bf069784a4ec634\",\r\n\"input\":\"0x\",\r\n\"nonce\":\"0x95e8\",\r\n\"r\":\"0x1c79013f8efbb2e4dce3d29e3626f08df16247b5069e58a88584878235d89f03\",\r\n\"s\":\"0x168a66cf4819a5663e32eb09535b005211c9daae7bd25bde58e0e7f43f02adbf\",\r\n\"to\":\"0x15776a03ef5fdf2a54a1b3a991c1641d0bfa39e7\",\r\n\"transactionIndex\":\"0x3f\",\r\n\"v\":\"0x1c\",\r\n\"value\":\"0xf17937cf93cc0000\"\r\n}\r\n],\r\n\"transactionsRoot\":\"0x763b7ce8c287762d56b5f8b713424512dc3df5e5d2676578c3bfec50d6f20981\",\r\n\"uncles\":[]\r\n}\r\n}";

		//	var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
		//	mockHttpMessageHandler.Protected()
		//		.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
		//		.ReturnsAsync(new HttpResponseMessage
		//		{
		//			StatusCode = HttpStatusCode.OK,
		//			Content = new StringContent(infuraSampleResponse),
		//		});

		//	var client = new HttpClient(mockHttpMessageHandler.Object);
		//	mockHttpClientFactory.Setup(m => m.CreateClient(It.IsAny<string>())).Returns(client);

		//	mockSettings.Setup(m => m.Value)
		//		.Returns(new InfuraSettings { BaseUrl = "http://infuratest", ProjectId = "testprojectid" })
		//		.Verifiable();

		//	var transactionSearchRequest = new TransactionSearchRequest
		//	{
		//		Address = "0xc55eddadeeb47fcde0b3b6f25bd47d745ba7e7fa",
		//		BlockNumber = 9148873
		//	};

		//	var infuraTransactionSearchService = new InfuraTransactionSearchService(null, mockSettings.Object, mockHttpClientFactory.Object, mockLogger.Object);

		//	//Act
		//	var result = await infuraTransactionSearchService.Search(transactionSearchRequest);

		//	//Assert
		//	Assert.Equal(2, result.Transactions.Count);

		//	Assert.Equal("0x6dbde4b224013c46537231c548bd6ff8e2a2c927c435993d351866d505c523f1", result.Transactions[0].BlockHash);
		//	Assert.Equal(9148873, result.Transactions[0].BlockNumber);
		//	Assert.Equal("0xc55eddadeeb47fcde0b3b6f25bd47d745ba7e7fa", result.Transactions[0].From);
		//	Assert.Equal("0x59422595656a6b7c8917625607934d0e11fa0c40", result.Transactions[0].To);
		//	Assert.Equal(0.000000000000021M, result.Transactions[0].Gas);
		//	Assert.Equal("0x1fd509bc8a1f26351400f4ca206dbe7b8ebb8dcf3925ddf7201e8764e1bd3ea3", result.Transactions[0].Hash);
		//	Assert.Equal(80M, result.Transactions[0].Value);
		//	Assert.Equal(62, result.Transactions[0].TransactionIndex);

		//	Assert.Equal("0x6dbde4b224013c46537231c548bd6ff8e2a2c927c435993d351866d505c523f1", result.Transactions[1].BlockHash);
		//	Assert.Equal(9148873, result.Transactions[1].BlockNumber);
		//	Assert.Equal("0xc55eddadeeb47fcde0b3b6f25bd47d745ba7e7fa", result.Transactions[1].From);
		//	Assert.Equal("0x15776a03ef5fdf2a54a1b3a991c1641d0bfa39e7", result.Transactions[1].To);
		//	Assert.Equal(0.000000000000021M, result.Transactions[1].Gas);
		//	Assert.Equal("0xfcbbca93ff416601e5be95838fcfa2c534c48027b10172c12bf069784a4ec634", result.Transactions[1].Hash);
		//	Assert.Equal(17.4M, result.Transactions[1].Value);
		//	Assert.Equal(63, result.Transactions[1].TransactionIndex);

		//	mockSettings.VerifyAll();
		//	mockHttpClientFactory.VerifyAll();
		//	mockLogger.VerifyAll();
		//}
	}
}
