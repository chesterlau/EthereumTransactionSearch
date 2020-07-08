using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EthereumTransactionSearch.Models;
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
        private readonly ILogger<SearchController> _logger;
        private readonly InfuraSettings _infuraSettings;

        public SearchController(IOptions<InfuraSettings> infuraSettings, ILogger<SearchController> logger)
        {
            _logger = logger;
            _infuraSettings = infuraSettings.Value;
        }

        [HttpPost, Route("/api/search/Transactions")]
        [ProducesResponseType(typeof(TransactionSearchResponse), 200)]
        [ProducesResponseType(typeof(ApiResult), 400)]
        public async Task<IActionResult> SearchTransactions()
        {
            _logger.LogInformation("Test");
            return Ok();
        }
    }
}