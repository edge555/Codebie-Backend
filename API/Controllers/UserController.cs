using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> GetUserByIdAsync(string GuId)
        {
            var userDto = await _userService.GetUserByIdAsync(GuId);
            if (userDto == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(userDto);
        }
        [HttpPut("{GuId}"), Authorize]
        public async Task<IActionResult> UpdateUserByIdAsync(string GuId, [FromBody] UserUpdateDto userUpdateDto)
        {
            var userDto = await _userService.UpdateUserByIdAsync(GuId, userUpdateDto);
            if (userDto == null)
            {
                return BadRequest("Can not update user.");
            }
            return Ok(userDto);
        }
        [HttpDelete("{GuId}"), Authorize]
        public async Task<IActionResult> DeleteUserByIdAsync(string GuId)
        {
            var deleted = await _userService.DeleteUserByIdAsync(GuId);
            if (!deleted)
            {
                return BadRequest("Can not delete user.");
            }
            return NoContent();
        }
    }
}
