namespace Ṃeenkaran.Application.DTOs.User
{
    public class ActiveFishingFeedDto
    {
        public int Id { get; set; }

        public string SpotName { get; set; } = string.Empty;

        public string Area { get; set; } = string.Empty;

        public string FishType { get; set; } = string.Empty;

        public int ActiveBoats { get; set; }

        public string Weather { get; set; } = string.Empty;

        public bool IsHotSpot { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string GuideName { get; set; } = string.Empty;
    }
}
