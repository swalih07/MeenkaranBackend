namespace Ṃeenkaran.Application.DTOs.GuidePackage
{
    public class GuidePackageDto
    {
        public int Id { get; set; }

        public string PackageName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal price { get; set; }

        public int DurationHours { get; set; }
        
        public string Includes { get; set; }=string.Empty;

        public string TripLocation { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
