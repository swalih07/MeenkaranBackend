using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Domain.Entities.User;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ṃeenkaran.Application.Services
{
    public class CatchPostService:ICatchPostService
    {
        private readonly AppDbContext _context;
        private readonly CloudinaryService _cloudinaryService;

        public CatchPostService(AppDbContext context,CloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<string>CreatePostAsync(CreateCatchPostDto dto)
        {
            var imageUrl = await _cloudinaryService.UploadImageAsync(dto.Image);

            var post = new CatchPost
            {
                UserId = dto.UserId,
                ImageUrl = imageUrl,
                Description = dto.Description,
                FishType = dto.FishType,
                Location = dto.Location,
            };
            _context.CatchPosts.Add(post);
            await _context.SaveChangesAsync();
            return "Catch post Uploaded Successfully";
        }

        public async Task<List<CatchFeedDto>>GetCommunityFeedAsync(string location)
        {
            return await _context.CatchPosts
                .Include(x => x.User)
                .Where(x => x.Location.Contains(location))
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new CatchFeedDto
                {
                    Id = x.Id,
                    UserName = x.User.Name,
                    UserProfileImage = x.User.ProfileImageUrl,
                    ImageUrl = x.ImageUrl,
                    Description = x.Description,
                    FishType = x.FishType,
                    Location = x.Location,
                    CreatedAt = x.CreatedAt

                })
                .ToListAsync();
        }
    }
}
