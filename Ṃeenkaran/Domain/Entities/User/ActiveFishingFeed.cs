namespace Ṃeenkaran.Domain.Entities.User
{
    public class ActiveFishingFeed
    {
        public int Id { get; set; }

        public string SpotName { get; set; } = string.Empty;

        public string Area { get; set; } = string.Empty;

        public string FishType { get; set; } = string.Empty;

        public int ActiveBoats { get; set; }

        public string Weather { get; set; } = string.Empty;

        public bool IsHotSpot { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int GuideId { get; set; }

        public Guide Guide { get; set; }
    }
}
