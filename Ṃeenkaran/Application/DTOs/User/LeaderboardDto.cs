namespace Ṃeenkaran.Application.DTOs.User
{
    public class LeaderboardDto
    {
        public int UserId { get; set; }

        public string Name { get; set; }=string.Empty;

        public string ProfileImage { get; set; }=string.Empty;

        public int Points { get; set; }

        public string Badge { get; set; } = string.Empty;
    }
}
