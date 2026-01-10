using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Users.Commands.CreateUser;

namespace TaskManager.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] UserCreateCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                return StatusCode(201, result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation error: {Username}", command.Name);
                return StatusCode(400, new UserCreateResponse
                {
                    Message = ex.Message
                });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error registering user: {Username}", command.Name);
                return StatusCode(500, new UserCreateResponse
                {
                    Message = "Unexpected server error"
                });
            }
        }
    }
}
