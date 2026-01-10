namespace TaskManager.Domain
{
    public class Task
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Priority Priority { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreateDate { get; set; }
        public User User { get; set; } = null!;
    }

    public enum Priority
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
}
