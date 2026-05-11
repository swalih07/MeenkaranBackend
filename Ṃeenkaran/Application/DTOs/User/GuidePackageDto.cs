namespace Ṃeenkaran.Application.DTOs.User
{
    public class GuidePackageDto
    {
        public int Id { get; set; }
        public string PackageName { get; set; } = string.Empty;

        public string Description { get; set; }=string.Empty;

        public decimal Price { get; set; }

        public int DurationHours { get; set; }

        public string Includes { get; set; } = string.Empty;
    }
}
