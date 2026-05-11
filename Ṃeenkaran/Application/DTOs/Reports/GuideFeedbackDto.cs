namespace Ṃeenkaran.Application.DTOs.Reports
{
    public class GuideFeedbackDto
    {
        public int id {  get; set; }
        public int GuideId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int GuideBookingId { get; set; }
        public int Rating {  get; set; }
        public string Comment { get; set; }=string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
