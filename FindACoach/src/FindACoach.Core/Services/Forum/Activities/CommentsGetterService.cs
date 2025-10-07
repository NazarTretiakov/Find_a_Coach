using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Forum;
using FindACoach.Core.ServiceContracts.Forum.Activities;

namespace FindACoach.Core.Services.Forum.Activities
{
    public class CommentsGetterService : ICommentsGetterService
    {
        private readonly ICommentsRepository _commentsRepository;

        public CommentsGetterService(ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }

        public async Task<List<CommentToResponse>> GetCommentsPaged(string activityId, int page, int pageSize)
        {
            return await _commentsRepository.GetCommentsPaged(activityId, page, pageSize);
        }
    }
}
