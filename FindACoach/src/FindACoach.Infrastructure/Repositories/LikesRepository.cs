using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Forum;
using FindACoach.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace FindACoach.Infrastructure.Repositories
{
    public class LikesRepository : ILikesRepository
    {
        private readonly ApplicationDbContext _db;

        public LikesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<LikesInformationToResponse> AddLike(string userId, string activityId)
        {
            await _db.Likes.AddAsync(new Like()
            {
                Id = Guid.NewGuid(),
                ActivityId = Guid.Parse(activityId),
                UserId = Guid.Parse(userId)
            });

            await _db.SaveChangesAsync();

            int numberOfLikes = await _db.Likes.CountAsync(l => l.ActivityId == Guid.Parse(activityId));

            return new LikesInformationToResponse() { IsLiked = true, NumberOfLikes = numberOfLikes };
        }

        public async Task<Like> GetLike(string userId, string activityId)
        {
            return await _db.Likes.FirstOrDefaultAsync(l => l.UserId == Guid.Parse(userId) && l.ActivityId == Guid.Parse(activityId));
        }

        public async Task<bool> IsActivityLikedByUser(string userId, string activityId)
        {
            Like like = await _db.Likes.FirstOrDefaultAsync(l => l.UserId == Guid.Parse(userId) && l.ActivityId == Guid.Parse(activityId));

            return like != null;
        }

        public async Task<LikesInformationToResponse> RemoveLike(Like like)
        {
            _db.Likes.Remove(like);

            await _db.SaveChangesAsync();

            int numberOfLikes = await _db.Likes.CountAsync(l => l.ActivityId == like.ActivityId);

            return new LikesInformationToResponse() { IsLiked = false, NumberOfLikes = numberOfLikes };
        }
    }
}