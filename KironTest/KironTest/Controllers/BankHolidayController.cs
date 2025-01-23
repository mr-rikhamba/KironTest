using KironTest.Logic.Contracts;
using KironTest.Logic.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KironTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [SimpleCache(1)]
    public class BankHolidayController(IBankHolidayContract _bankHolidayService, BankServiceManager _bankServiceManager) : ControllerBase
    {
        [HttpGet("RetrieveData")]
        public async Task<IActionResult> Get()
        {
            await _bankHolidayService.UpdateHolidayData();

            return Ok();
        }
        [HttpGet("GetRegions")]
        public async Task<IActionResult> GetRegions()
        {
            return Ok(await _bankHolidayService.GetRegions());
        }
        [HttpGet("GetRegionHolidays")]
        public async Task<IActionResult> GetRegionHolidays([FromQuery] int regionId)
        {
            return Ok(await _bankHolidayService.GetRegionHolidays(regionId));
        }

        [HttpPost("EnableService")]
        public async Task<IActionResult> EnableService()
        {
            if (!_bankServiceManager.IsServiceEnabled)
            {
                _bankServiceManager.EnableService();
                return Ok();
            }
            return BadRequest("The service is currently enabled");
        }
        [HttpPost("DisableService")]
        public async Task<IActionResult> DisableServce()
        {
            if (_bankServiceManager.IsServiceEnabled)
            {
                _bankServiceManager.DisableService();
                return Ok();
            }
            return BadRequest("The service is currently disabled");
        }
    }
}
