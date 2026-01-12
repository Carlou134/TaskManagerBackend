using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Application.Tasks.Commands.DeleteTask
{
    public class TaskDeleteCommandHandler : IRequestHandler<TaskDeleteCommand, TaskDeleteMessage>
    {
        private readonly AppDbContext _context;

        public TaskDeleteCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskDeleteMessage> Handle(TaskDeleteCommand request, CancellationToken cancellationToken)
        {
            var deleteTask = await _context.Task.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (deleteTask == null)
            {
                throw new UnauthorizedAccessException("Invalid task");
            }

            if (request.UserId != deleteTask.UserId)
            {
                throw new UnauthorizedAccessException("You don't have access");
            }

            _context.Remove(deleteTask);
            await _context.SaveChangesAsync(cancellationToken);

            return new TaskDeleteMessage
            {
                Message = "Task deleted successfully"
            };
        }
    }
}
