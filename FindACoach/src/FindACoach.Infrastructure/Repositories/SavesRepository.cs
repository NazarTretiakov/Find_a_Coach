using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Forum;
using FindACoach.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace FindACoach.Infrastructure.Repositories
{
    public class SavesRepository : ISavesRepository
    {
        private readonly ApplicationDbContext _db;

        public SavesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<SaveInformationToResponse> AddSave(string userId, string activityId)
        {
            await _db.Saves.AddAsync(new Save()
            {
                Id = Guid.NewGuid(),
                ActivityId = Guid.Parse(activityId),
                UserId = Guid.Parse(userId)
            });

            await _db.SaveChangesAsync();

            Activity savedActivity = await _db.Activities.FirstOrDefaultAsync(a => a.Id == Guid.Parse(activityId));

            return new SaveInformationToResponse() { IsSaved = true };
        }

        public async Task<Save> GetSave(string userId, string activityId)
        {
            return await _db.Saves.FirstOrDefaultAsync(l => l.UserId == Guid.Parse(userId) && l.ActivityId == Guid.Parse(activityId));
        }

        public async Task<bool> IsActivitySavedByUser(string userId, string activityId)
        {
            Save save = await _db.Saves.FirstOrDefaultAsync(l => l.UserId == Guid.Parse(userId) && l.ActivityId == Guid.Parse(activityId));

            return save != null;
        }

        public async Task<SaveInformationToResponse> RemoveSave(Save save)
        {
            _db.Saves.Remove(save);

            await _db.SaveChangesAsync();

            Activity savedActivity = await _db.Activities.FirstOrDefaultAsync(a => a.Id == Guid.Parse(save.ActivityId.ToString()));

            return new SaveInformationToResponse() { IsSaved = false };
        }
    }
}