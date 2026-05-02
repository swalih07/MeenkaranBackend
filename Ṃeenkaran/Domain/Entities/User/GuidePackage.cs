namespace Ṃeenkaran.Domain.Entities.User
{
    public class GuidePackage
    {
        public int Id { get; set; }

        public string PackageName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Duration { get; set; } = string.Empty;

        public string Includes { get; set; } = string.Empty;

        public int GuideId { get; set; }

        public Guide Guide { get; set; }
    }
}