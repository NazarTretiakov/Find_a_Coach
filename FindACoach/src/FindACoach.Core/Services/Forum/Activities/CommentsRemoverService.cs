using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.ServiceContracts.Forum.Activities;

namespace FindACoach.Core.Services.Forum.Activities
{
    public class CommentsRemoverService : ICommentsRemoverService
    {
        private readonly ICommentsRepository _commentsRepository;

        public CommentsRemoverService(ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }

        public async Task DeleteComment(string commentId, string userId)
        {
            await _commentsRepository.DeleteComment(commentId, userId);
        }
    }
}
