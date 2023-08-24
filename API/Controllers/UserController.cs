using Microsoft.AspNetCore.Mvc;
using Service.Dtos.User;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) 
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersAsync()
        {
            return Ok(await _userService.GetUsersAsync());
        }
        [HttpGet("{GuId}")]
        public async Task<IActionResult> GetUserByGuIdAsync(string GuId)
        {
            var userDto = await _userService.GetUserByGuIdAsync(GuId);
            return Ok(userDto);
        }
    }
}
