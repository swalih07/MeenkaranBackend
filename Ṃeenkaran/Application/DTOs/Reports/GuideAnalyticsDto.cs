namespace Ṃeenkaran.Application.DTOs.Reports
{
    public class GuideAnalyticsDto
    {
        public int GuideId { get; set; }
        public int TotalTrips { get; set; }
        public int PendingTrips { get; set; }
        public int AcceptedTrips{  get; set; }
        public int RejectedTrips { get; set; }
        public int CompletedTrips { get; set; }
        public int CancelledTrips { get; set; }


        public decimal TotalEarnings { get; set; }
        public double AverageRatings { get; set; }
        public int TotalFeedbackCount { get; set; }
    }
}
