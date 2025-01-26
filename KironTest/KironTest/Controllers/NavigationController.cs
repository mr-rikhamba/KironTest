using KironTest.Logic.Contracts;
using KironTest.Logic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KironTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavigationController(INavigationContract _navService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            return Ok(await _navService.GetNavigation());
        }
    }
}
