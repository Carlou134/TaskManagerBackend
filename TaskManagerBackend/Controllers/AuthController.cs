using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Auth.Dtos;
using TaskManager.Application.Auth.Interfaces;

namespace TaskManager.Api.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService!;
        }

        [HttpPost("/authenticate")]
        public async Task<ActionResult> Authenticate([FromBody] LoginRequestDto request)
        {
            try
            {
                CancellationTokenSource tokenSource = new CancellationTokenSource();
                return Ok(await _authService.Login(request, tokenSource.Token).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
