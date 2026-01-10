namespace TaskManager.Application.Auth.Dtos
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public int ExpireMinutes { get; set; }
    }
}
