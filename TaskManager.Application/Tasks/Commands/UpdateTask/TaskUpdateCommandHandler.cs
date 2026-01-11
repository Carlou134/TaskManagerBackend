using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Application.Tasks.Commands.UpdateTask
{
    public class TaskUpdateCommandHandler : IRequestHandler<TaskUpdateCommand, TaskUpdateMessage>
    {
        private readonly AppDbContext _context;

        public TaskUpdateCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskUpdateMessage> Handle(TaskUpdateCommand request, CancellationToken cancellationToken)
        {
            var updateTask = await _context.Task.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if(updateTask == null)
            {
                throw new UnauthorizedAccessException("Invalid task");
            }

            if(request.UserId != updateTask.UserId)
            {
                throw new UnauthorizedAccessException("You don't have access");
            }

            updateTask.Name = request.Name;
            updateTask.Description = request.Description;
            updateTask.IsCompleted = request.IsCompleted;
            updateTask.Priority = request.Priority;

            await _context.SaveChangesAsync(cancellationToken);

            return new TaskUpdateMessage
            {
                Message = "Task updated successfully"
            };
        }
    }
}
