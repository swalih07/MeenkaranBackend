using Ṃeenkaran.Domain.Enums;

namespace Ṃeenkaran.Domain.Entities.User
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string? ProfileImageUrl { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public int Points { get; set; } = 0;

        public BadgeType Badge { get; set; } = BadgeType.Beginner;

        public string? ResetOtp { get; set; }

        public DateTime? ResetOtpExpiry { get; set; }

        public bool IsEmailVerified { get; set; } = false;

        public string? EmailOtp { get; set; }

        public DateTime? EmailOtpExpiryTime { get; set; }

        public bool IsEmailOtpUsed { get; set; } = false;

        public string Role { get; set; } = "User";

        public bool IsBlocked { get; set; }

        public string? BlockReason { get; set; }
    }
}