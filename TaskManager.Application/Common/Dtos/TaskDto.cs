using TaskManager.Domain;

namespace TaskManager.Application.Common.Dtos
{
    public class TaskDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Priority Priority { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
