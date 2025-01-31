using Ecommerce.Core.Interfaces;
using Ecommerce.Api.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.Api.Controllers
{
    [Route("api/auth")]
    [ApiController] 
    public class AuthController : ControllerBase  
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            var result = await _authService.Register(model.Username, model.Password, model.Role);
            if (!result) return BadRequest(new { message = "L'utilisateur existe déjà" });

            return Ok(new { message = "Inscription réussie" });
        }
    }
}
