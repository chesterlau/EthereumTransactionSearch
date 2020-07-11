using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EthereumTransactionSearch.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class HealthController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}