using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Domain.Entities.User;
using Ṃeenkaran.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ṃeenkaran.Application.Services
{
    public class PostInteractionService:IPostInteractionService
    {
        private readonly AppDbContext _context;
        public PostInteractionService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string>ToggleLikeAsync(CreateLikeDto dto)
        {
            var existing = await _context.PostLikes
                .FirstOrDefaultAsync(x =>
                    x.UserId == dto.UserId &&
                    x.CatchPostId == dto.PostId);

            if(existing != null)
            {
                _context.PostLikes.Remove(existing);
                await _context.SaveChangesAsync();
                return "Unliked";
            }

            var Like = new PostLike
            {
                UserId = dto.UserId,
                CatchPostId = dto.PostId
            };

            _context.PostLikes.Add(Like);
            await _context.SaveChangesAsync();
            return "Liked";
        }
        public async Task<string>AddCommentAsync(CreateCommentDto dto)
        {
            var comment = new PostComment
            {
                UserId = dto.UserId,
                CatchPostId = dto.PostId,
                Comment = dto.Comment
            };
            _context.PostComments.Add(comment);
            await _context.SaveChangesAsync();
            return "Comment added";
        }
        public async Task<List<PostComment>>GetCommentsAsync(int postId)
        {
            return await _context.PostComments
                .Where(x => x.CatchPostId == postId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}
