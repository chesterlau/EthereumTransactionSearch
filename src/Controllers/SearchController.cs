using EthereumTransactionSearch.Models;
using EthereumTransactionSearch.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EthereumTransactionSearch.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : Controller
    {
        private readonly ITransactionSearchService _transactionSearchService;
        private readonly ILogger<SearchController> _logger;

        public SearchController(ITransactionSearchService transactionSearchService, ILogger<SearchController> logger)
        {
            _logger = logger;
            _transactionSearchService = transactionSearchService;
        }

        [HttpPost, Route("/api/search/Transactions")]
        [ProducesResponseType(typeof(TransactionSearchResponse), 200)]
        [ProducesResponseType(typeof(ApiResult), 400)]
        public async Task<IActionResult> SearchTransactions(TransactionSearchRequest transactionSearchRequest)
        {
            try
            {
                _logger.LogInformation("/api/search/Transactions");

                var result = await _transactionSearchService.Search(transactionSearchRequest);

                _logger.LogInformation("Returning response");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception caught!");
                return BadRequest(new ApiResult { Title = "An error has occured" });
            }
        }
    }
}