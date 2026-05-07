using Ṃeenkaran.Application.DTOs.User;
using Ṃeenkaran.Domain.Entities.User;

namespace Ṃeenkaran.Application.Interfaces
{
    public interface IPostInteractionService
    {
        Task<string> ToggleLikeAsync(CreateLikeDto dto);
        Task<string> AddCommentAsync(CreateCommentDto dto);
        Task<List<PostComment>> GetCommentsAsync(int postId);
    }
}
