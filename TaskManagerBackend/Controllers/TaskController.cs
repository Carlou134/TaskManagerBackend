using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.Application.Tasks.Commands.CreateTask;
using TaskManager.Application.Tasks.Commands.DeleteTask;
using TaskManager.Application.Tasks.Commands.UpdateTask;
using TaskManager.Application.Tasks.Queries;

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
        [HttpGet("list")]
        public async Task<ActionResult> ListTasks()
        {
            try
            {
                return Ok(await _taskQueryService.GetAllTasks(GetCurrentUserId()));
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
                command.UserId = GetCurrentUserId();
                var result = await _mediator.Send(command, token);
                return StatusCode(201, result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation error: {taskName}", command.Name);
                return StatusCode(400, new
                {
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering task: {taskName}", command.Name);
                return StatusCode(500, new
                {
                    Message = "Unexpected server error"
                });
            }
        }

        [Authorize(Roles = "USER")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateTask([FromBody] TaskUpdateCommand command, CancellationToken token)
        {
            try
            {
                command.UserId = GetCurrentUserId();
                var result = await _mediator.Send(command, token);
                return StatusCode(201, result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation error: {taskName}", command.Name);
                return StatusCode(400, new
                {
                    Message = ex.Message
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating tasks: {taskName}", command.Name);
                return StatusCode(500, new
                {
                    Message = "Unexpected server error"
                });
            }
        }

        [Authorize(Roles = "USER")]
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteTask([FromBody] TaskDeleteCommand command, CancellationToken token)
        {
            try
            {
                command.UserId = GetCurrentUserId();
                var result = await _mediator.Send(command, token);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation error: {Id}", command.Id);
                return StatusCode(400, new
                {
                    Message = ex.Message
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting tasks: {Id}", command.Id);
                return StatusCode(500, new
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
