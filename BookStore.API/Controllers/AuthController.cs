using BookStore.Application.DTOs.Auth;
using BookStore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
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
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);
            if (!result.IsAuthenticated) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _authService.GetTokenAsync(model);
            if (!result.IsAuthenticated) return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole([FromQuery] string userId, [FromQuery] string roleName)
        {
            var result = await _authService.AddRoleAsync(userId, roleName);
            if (result != "Role added successfully!") return BadRequest(result);

            return Ok(result);
        }
    }
}