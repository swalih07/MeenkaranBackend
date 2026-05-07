using Ṃeenkaran.Domain.Enums;
using System.Data;

namespace Ṃeenkaran.Domain.Entities.User
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; }= string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string ProfileImageUrl { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }
        public int Points { get; set; } = 0;
        public BadgeType Badge { get; set; } = BadgeType.Beginner;
        public string? ResetOtp { get; set; }
        public DateTime? ResetOtpExpiry { get; set; }
    }
}
