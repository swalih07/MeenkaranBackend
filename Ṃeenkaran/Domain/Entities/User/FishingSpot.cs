namespace Ṃeenkaran.Domain.Entities.User
{
    public class FishingSpot
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Latitude { get; set; } 
        public double Longitude { get; set; }
        public string SpotType { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

    }
}
