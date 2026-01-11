using MediatR;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Application.Tasks.Commands.CreateTask
{
    public class TaskCreateCommandHandler : IRequestHandler<TaskCreateCommand, TaskCreateMessage>
    {
        private readonly AppDbContext _context;

        public TaskCreateCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskCreateMessage> Handle(TaskCreateCommand request, CancellationToken cancellationToken)
        {
            var newTask = new TaskManager.Domain.Task
            {
                Name = request.Name,
                UserId = request.UserId,
                IsCompleted = request.IsCompleted,
                Description = request.Description,
                Priority = request.Priority,
                CreateDate = DateTime.UtcNow
            };

            _context.Add(newTask);
            await _context.SaveChangesAsync(cancellationToken);
            return new TaskCreateMessage
            {
                Message = "Task successfully created"
            };
        }
    }
}
