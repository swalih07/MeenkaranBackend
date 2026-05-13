namespace Ṃeenkaran.Application.DTOs.Financial
{
    public class PlatformOverviewDto
    {
        public int ActiveUsers { get; set; }
        public int ActiveGuides { get; set; }
        public int SuccessfulTrips { get; set; }

        public decimal TotalCommissionEarned { get; set; }
        public decimal PendingGuidePayouts { get; set; }
        public decimal SentGuidePayouts { get; set; }
    }
}
