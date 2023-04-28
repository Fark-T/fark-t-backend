using AutoMapper;
using fark_t_backend.Dto;
using fark_t_backend.Models;
using fark_t_backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fark_t_backend.Controllers
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

        [HttpPost("register")]
        public async Task<ActionResult<Users>> Register(CreateUserDto request)
        {

            var user = await _authService.Register(request);
            if(user.IsFailed)
            {
                return BadRequest();
            }
 
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenDto>> Login(LoginDto request)
        {
            var token = await _authService.Login(request);    
            if (token.IsFailed)
            {
                return BadRequest("Wrong user or password.");
            }

            return Ok(token.Value);
        }

    }
}
