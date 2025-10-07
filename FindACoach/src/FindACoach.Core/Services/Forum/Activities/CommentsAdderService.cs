using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Forum;
using FindACoach.Core.ServiceContracts.Forum.Activities;

namespace FindACoach.Core.Services.Forum.Activities
{
    public class CommentsAdderService : ICommentsAdderService
    {
        private readonly ICommentsRepository _commentsRepository;

        public CommentsAdderService(ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }

        public async Task<CommentToResponse> CreateComment(AddCommentDTO dto)
        {
            Comment newComment = new Comment()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(dto.UserId),
                ActivityId = Guid.Parse(dto.ActivityId),
                Content = dto.Content,
                DateOfCreation = DateTime.Now
            };

            CommentToResponse commentToResponse = await _commentsRepository.AddComment(newComment);

            return commentToResponse;
        }
    }
}
