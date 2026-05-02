using System.Collections.Generic;

namespace Ṃeenkaran.Domain.Entities.User
{
    public class Guide
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ProfileImageUrl { get; set; } = string.Empty;

        public string Area { get; set; } = string.Empty;

        public string FishingStyle { get; set; } = string.Empty;

        public int ExperienceYears { get; set; }

        public decimal PricePerDay { get; set; }

        public double Rating { get; set; }

        public bool IsAvailable { get; set; } = true;

        public ICollection<GuidePackage> Packages { get; set; }
            = new List<GuidePackage>();
    }
}