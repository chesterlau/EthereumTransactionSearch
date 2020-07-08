using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EthereumTransactionSearch.Models;
using Microsoft.AspNetCore.Mvc;

namespace EthereumTransactionSearch.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : Controller
    {
        [HttpPost, Route("/api/search/Transactions")]
        [ProducesResponseType(typeof(TransactionSearchResponse), 200)]
        [ProducesResponseType(typeof(ApiResult), 400)]
        public async Task<IActionResult> SearchTransactions()
        {
            return Ok();
        }
    }
}