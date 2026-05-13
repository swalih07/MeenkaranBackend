namespace Ṃeenkaran.Application.DTOs.User
{
    public class CreateCatchPostDto
    {
        public IFormFile Image { get; set; }

        public string Description { get; set; }= string.Empty;

        public string FishType { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;
    }
}
