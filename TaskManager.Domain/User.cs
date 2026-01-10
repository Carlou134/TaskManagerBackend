namespace TaskManager.Domain
{
    public class User
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public Role Role { get; set; } = null!;

        private readonly List<TaskManager.Domain.Task> _tasks = new();
        public IReadOnlyCollection<TaskManager.Domain.Task> Tasks => _tasks;
    }
}
