namespace Ṃeenkaran.Application.DTOs.Admin
{
    public class SystemErrorLogDto
    {
        public int Id { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public string? StackTrace { get; set; }
        public string? Source { get; set; }
        public string? RequestPath { get; set; }
        public string? HttpMethod { get; set; }
        public int? UserId { get; set; }
        public string? UserEmail { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
