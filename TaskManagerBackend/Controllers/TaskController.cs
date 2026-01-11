
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.Application.Tasks.Commands.CreateTask;
using TaskManager.Application.Tasks.Queries;
using TaskManager.Application.Users.Commands.CreateUser;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;
        private readonly ITaskQueryService _taskQueryService;

        public TaskController(IMediator mediator, ILogger<UserController> logger, ITaskQueryService taskQueryService)
        {
            _mediator = mediator;
            _logger = logger;
            _taskQueryService = taskQueryService;
        }

        [Authorize(Roles = "USER")]
        [HttpGet("list/{id}")]
        public async Task<ActionResult> ListTasks([FromRoute] int id)
        {
            try
            {
                if(id == GetCurrentUserId())
                {
                    return Ok(await _taskQueryService.GetAllTasks(id));
                }

                return Unauthorized("No tiene acceso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "USER")]
        [HttpPost("register")]
        public async Task<ActionResult> RegisterTask([FromBody] TaskCreateCommand command, CancellationToken token)
        {
            try
            {
                var result = await _mediator.Send(command, token);
                return StatusCode(201, result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation error: {taskName}", command.Name);
                return StatusCode(400, new UserCreateResponse
                {
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user: {Username}", command.Name);
                return StatusCode(500, new UserCreateResponse
                {
                    Message = "Unexpected server error"
                });
            }
        }

        protected int GetCurrentUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
    }
}
