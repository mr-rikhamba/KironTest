using KironTest.Logic.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KironTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SimpleCache(60)]
    public class CoinController(ILogger<CoinController> _logger, ICoinContract _coinService) : ControllerBase
    {
        [HttpGet("GetCoinStats")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _coinService.GetCoins());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
    }
}
