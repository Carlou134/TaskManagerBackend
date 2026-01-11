using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Common.Dtos;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Application.Tasks.Queries
{
    public interface ITaskQueryService
    {
        Task<IReadOnlyCollection<TaskDto>> GetAllTasks(int id);
    }

    public class TaskQueryService : ITaskQueryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TaskQueryService(
            AppDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<TaskDto>> GetAllTasks(int id)
        {
            var tasks = await _context.Task.Where(x => x.UserId == id).Include(x => x.User).ToListAsync();
            return _mapper.Map<List<TaskDto>>(tasks);
        }
    }
}
