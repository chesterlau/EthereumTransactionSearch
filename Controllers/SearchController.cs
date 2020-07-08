using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EthereumTransactionSearch.Models;
using EthereumTransactionSearch.Services;
using EthereumTransactionSearch.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
            _logger.LogInformation("/api/search/Transactions");

            //TODO model is valid

            await _transactionSearchService.Search(transactionSearchRequest);

            return Ok();
        }
    }
}