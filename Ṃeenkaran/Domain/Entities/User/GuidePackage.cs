using System.Globalization;

namespace Ṃeenkaran.Domain.Entities.User
{
    public class GuidePackage
    {
        public int Id { get; set; }
       
        public string PackageName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }


        public int DurationHours { get; set; }

        public string Includes { get; set; } = string.Empty;

        public string TripLocation { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int GuideId { get; set; }

        public Guide Guide { get; set; }
    }
}