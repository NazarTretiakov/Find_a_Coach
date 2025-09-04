using FindACoach.Core.Domain.IdentityEntities;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO;
using FindACoach.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace FindACoach.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _db;

        public UsersRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task ChangeCompleteProfileWindowState(string userId, bool isVisible, string title)
        {
            User activeUser = await _db.Users.FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));

            activeUser.IsCompleteProfileWindowVisible = isVisible;
            activeUser.CompleteProfileWindowTitle = title;

            await _db.SaveChangesAsync();
        }

        public async Task<CompleteProfileWindowStateDTO> GetCompleteProfileWindowState(string userId)
        {
            User activeUser = await _db.Users.FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));

            CompleteProfileWindowStateDTO completeProfileWindowStateDTO = new CompleteProfileWindowStateDTO()
            {
                IsVisible = activeUser.IsCompleteProfileWindowVisible,
                Title = activeUser.CompleteProfileWindowTitle
            };

            return completeProfileWindowStateDTO;
        }
    }
}
