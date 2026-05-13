namespace Ṃeenkaran.Infrastructure.Razorpay
{
    public class Settings
    {
        public string KeyId { get; set; } = string.Empty;
        public string KeySecret { get; set; } = string.Empty;

        public string Currency { get; set; } = "INR";
        public string ApiBaseUrl { get; set; } = "https://api.razorpay.com/v1";
    }
}
