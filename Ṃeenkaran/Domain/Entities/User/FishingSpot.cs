namespace Ṃeenkaran.Domain.Entities.User
{
    public class FishingSpot
    {
        public int Id { get; set; }


        public int GuideId { get; set; }
        public Guide Guide { get; set; }


        public  string SpotName { get; set; } = string.Empty;

        //public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string LocationName { get; set; } = string.Empty;
        public double Latitude { get; set; } 
        public double Longitude { get; set; }

        public bool IsHotspot { get; set; } = true;
        public string SpotType { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;


        public bool IsApproved { get; set; } = false;
        public bool IsRemoved { get; set; } = false;

        public int? ReviewedByAdminId { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? RejectedAt { get; set; }
        public DateTime? RemovedAt { get; set; }
        public string? RejectReason { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
