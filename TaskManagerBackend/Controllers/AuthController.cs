using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Auth.Commands.Login;

namespace TaskManager.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Authenticate([FromBody] LoginCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var token = await _mediator.Send(command, cancellationToken);
                return Ok(token);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation error: {Username}", command.UserName);
                return BadRequest(new
                {
                    Message = ex.Message,
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Failed login attempt: {Username}", command.UserName);
                return Unauthorized(new
                {
                    Message = ex.Message,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during login: {Username}", command.UserName);
                return StatusCode(500, new
                {
                    Message = "Unexpected server error"
                });
            }
        }
    }
}
