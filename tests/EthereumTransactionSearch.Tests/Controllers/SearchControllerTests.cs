using EthereumTransactionSearch.Controllers;
using EthereumTransactionSearch.Models;
using EthereumTransactionSearch.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EthereumTransactionSearch.Tests.Controllers
{
	public class SearchControllerTests
	{
		[Fact]
		public async Task Transactions_Returns_OkResponse_Correctly()
		{
			//Arrange
			Mock<ITransactionSearchService> mockTransactionSearchService = new Mock<ITransactionSearchService>();
			Mock<ILogger<SearchController>> mockLogger = new Mock<ILogger<SearchController>>();

			var transactionSearchRequest = new TransactionSearchRequest
			{ 
				Address = "0xc55eddadeeb47fcde0b3b6f25bd47d745ba7e7fa",
				BlockNumber = 1123
			};

			mockTransactionSearchService.Setup(m => m.Search(It.IsAny<TransactionSearchRequest>()))
				.ReturnsAsync(new TransactionSearchResponse 
				{
					Transactions = new List<TransactionResults>
					{
						new TransactionResults 
						{ 
							BlockHash = "0x6dbde4b224013c46537231c548bd6ff8e2a2c927c435993d351866d505c523f1",
							BlockNumber = 1123,
							From = "0xc55eddadeeb47fcde0b3b6f25bd47d745ba7e7fa",
							To = "0x59422595656a6b7c8917625607934d0e11fa0c40",
							Gas = 0.0000021m,
							Hash = "0x1fd509bc8a1f26351400f4ca206dbe7b8ebb8dcf3925ddf7201e8764e1bd3ea3",
							TransactionIndex = 1,
							Value = 123
						}
					}
				})
				.Verifiable();

			var controller = new SearchController(mockTransactionSearchService.Object, mockLogger.Object);

			//Act
			var response = await controller.SearchTransactions(transactionSearchRequest) as OkObjectResult;
			var responseObject = response.Value as TransactionSearchResponse;

			//Assert
			Assert.Equal(200, response.StatusCode);
			Assert.NotNull(responseObject);
			Assert.Single(responseObject.Transactions);

			mockTransactionSearchService.VerifyAll();
			mockLogger.VerifyAll();
		}

		[Fact]
		public async Task Transactions_Returns_BadResponse_If_Exception_Is_Thrown()
		{
			//Arrange
			Mock<ITransactionSearchService> mockTransactionSearchService = new Mock<ITransactionSearchService>();
			Mock<ILogger<SearchController>> mockLogger = new Mock<ILogger<SearchController>>();

			var transactionSearchRequest = new TransactionSearchRequest
			{
				Address = "0xc55eddadeeb47fcde0b3b6f25bd47d745ba7e7fa",
				BlockNumber = 1123
			};

			mockTransactionSearchService.Setup(m => m.Search(It.IsAny<TransactionSearchRequest>()))
				.ThrowsAsync(new Exception("Error occured"))
				.Verifiable();

			var controller = new SearchController(mockTransactionSearchService.Object, mockLogger.Object);

			//Act
			var response = await controller.SearchTransactions(transactionSearchRequest) as BadRequestObjectResult;

			//Assert
			Assert.Equal(400, response.StatusCode);
			Assert.Equal("An error has occured", response.Value);

			mockTransactionSearchService.VerifyAll();
			mockLogger.VerifyAll();
		}
	}
}
