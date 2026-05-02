namespace Ṃeenkaran.Application.DTOs.User
{
    public class GuidePackageDto
    {
        public string PackageName { get; set; } = string.Empty;

        public string Description { get; set; }=string.Empty;

        public decimal Price { get; set; }

        public string Duration { get; set; } = string.Empty;

        public string Includes { get; set; } = string.Empty;
    }
}
