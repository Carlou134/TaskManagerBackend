namespace TaskManager.Application.Common.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}
