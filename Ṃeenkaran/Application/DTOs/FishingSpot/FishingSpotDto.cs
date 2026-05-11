using Ṃeenkaran.Domain.Entities.User;

namespace Ṃeenkaran.Application.DTOs.FishingSpot
{
    public class FishingSpotDto
    {
        public int Id { get; set; }

        public int GuideId { get; set; }

        public string GuideName { get; set; }=string.Empty;

        public string SpotName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string LocationName { get; set; } = string.Empty;

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public bool IsHotspot { get; set; } = true;

        public string SpotType { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //public DateTime? UpdatedAt { get; set; }
    }
}
