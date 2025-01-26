using KironTest.Logic.Contracts;
using KironTest.Logic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KironTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(ILogger<UserController> _logger, IUserContract _userService) : ControllerBase
    {
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserModel userModel)
        {
            try
            {
                return Ok(await _userService.CreateUser(userModel));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
        [HttpPost("LoginUser")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                if (loginModel.IsValid)
                {
                    return Ok(await _userService.LoginUser(loginModel.Username, loginModel.Password));
                }
                return BadRequest("Invalid credentials entered.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
    }
}
