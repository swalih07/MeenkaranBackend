using System.Collections.Generic;

namespace Ṃeenkaran.Domain.Entities.User
{
    public class Guide
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;

        public string ProfileImageUrl { get; set; } = string.Empty;

        public string Area { get; set; } = string.Empty;

        public string FishingStyle { get; set; } = string.Empty;

        public int ExperienceYears { get; set; }

        public decimal PricePerDay { get; set; }
        public string IdProofUrl { get; set; } = string.Empty;
        public bool IsApproved { get; set; } = false;

        public double Rating { get; set; } = 0;

        public bool IsAvailable { get; set; } = true;

        public string RefreshToken { get; set; } = string.Empty;
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public string? PasswordResetOtp { get; set; }
        public DateTime? PasswordResetOtpExpiryTime { get; set; }
        public bool IsPasswordResetOtpUsed { get; set; } = false;

        public ICollection<GuidePackage> Packages { get; set; }
            = new List<GuidePackage>();



        public bool IsRejected { get; set; }
        public string RejectionReason { get; set; } = string.Empty;

        public bool IsBlocked { get; set; }
        public string BlockReason { get; set; } = string.Empty;
    }
}