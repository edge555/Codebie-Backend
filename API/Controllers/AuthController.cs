﻿using Microsoft.AspNetCore.Mvc;
using Service.Dtos.Auth;
using Service.Dtos.User;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Signup")]
        public async Task<ActionResult<UserDto>> SignupAsync(SignupDto request)
        {
            var userDto = await _authService.SignupAsync(request);
            if (userDto == null)
            {
                return BadRequest("Can not signup.");
            }
            return Created(nameof(SignupAsync), userDto);
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<UserWithTokenDto>> LoginAsync(LoginDto request)
        {
            var userWithToken = await _authService.LoginAsync(request);
            if (userWithToken == null)
            {
                return BadRequest("Can not login.");
            }
            return Ok(userWithToken);
        }
    }
}
