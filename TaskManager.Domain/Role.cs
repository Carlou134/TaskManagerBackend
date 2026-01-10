namespace TaskManager.Domain
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IReadOnlyCollection<User> Users { get; set; } = Array.Empty<User>();
    }
}
