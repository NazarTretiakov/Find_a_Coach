using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Forum;
using FindACoach.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FindACoach.Infrastructure.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {

        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public CommentsRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<CommentToResponse> AddComment(Comment comment)
        {
            await _db.Comments.AddAsync(comment);
            await _db.SaveChangesAsync();

            string serverUrl = _configuration.GetValue<string>("ServerUrl");

            CommentToResponse commentToResponse = await _db.Comments.Where(c => c.Id == comment.Id)
                                                                    .Select(c => new CommentToResponse()
                                                                    {
                                                                        CommentId = c.Id,
                                                                        ActivityId = c.ActivityId,
                                                                        UserId = c.UserId,
                                                                        UserEmail = c.User.Email,
                                                                        UserFirstName = c.User.FirstName,
                                                                        UserLastName = c.User.LastName,
                                                                        UserImagePath = $"{serverUrl}/Images/UserProfiles/{c.User.ImagePath}",
                                                                        DateOfCreation = c.DateOfCreation,
                                                                        Content = c.Content
                                                                    }).FirstOrDefaultAsync(c => c.CommentId == comment.Id);

            return commentToResponse;
        }

        public async Task DeleteComment(string commentId, string userId)
        {
            Comment comment = await _db.Comments
                .Where(c => c.Id == Guid.Parse(commentId))
                .Select(c => new Comment()
                {
                    Id = c.Id,
                    User = c.User
                })
                .FirstOrDefaultAsync(c => c.Id == Guid.Parse(commentId));

            if (comment == null)
            {
                throw new ArgumentNullException("Comment id is incorrect.");
            }
            if (comment.User.Id != Guid.Parse(userId))
            {
                throw new ArgumentException("Only creator of the comment can delete it.");
            }

            _db.Comments.Remove(comment);
            await _db.SaveChangesAsync();
        }

        public async Task<List<CommentToResponse>> GetCommentsPaged(string activityId, int page, int pageSize)
        {
            string serverUrl = _configuration.GetValue<string>("ServerUrl");

            var activityComments = await _db.Comments
                .Where(c => c.ActivityId == Guid.Parse(activityId))
                .OrderByDescending(a => a.DateOfCreation)
                .Skip(page * pageSize)
                .Take(pageSize)
                .Select(c => new CommentToResponse
                {
                    CommentId = c.Id,
                    ActivityId = c.ActivityId,
                    UserId = c.UserId,
                    UserEmail = c.User.Email,
                    UserFirstName = c.User.FirstName,
                    UserLastName = c.User.LastName,
                    UserImagePath = $"{serverUrl}/Images/UserProfiles/{c.User.ImagePath}",
                    DateOfCreation = c.DateOfCreation,
                    Content = c.Content
                })
                .ToListAsync();

            return activityComments;
        }
    }
}
