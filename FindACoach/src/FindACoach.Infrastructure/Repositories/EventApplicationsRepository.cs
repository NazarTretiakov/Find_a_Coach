using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Forum;
using FindACoach.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FindACoach.Infrastructure.Repositories
{
    public class EventApplicationsRepository : IEventApplicationsRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;

        public EventApplicationsRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task Add(EventApplication application)
        {
            if (await _db.EventApplications.FirstOrDefaultAsync(a => a.UserId == application.UserId && a.SearchPersonPanelId == application.SearchPersonPanelId) != null)
            {
                return;
            }

            _db.EventApplications.Add(application);

            await _db.SaveChangesAsync();
        }

        public Task<List<EventApplicationToResponse>> GetApplications(string searchPersonPanelId)
        {
            string serverUrl = _configuration.GetValue<string>("ServerUrl");

            var applications = _db.EventApplications
                .Where(a => a.SearchPersonPanelId == Guid.Parse(searchPersonPanelId))
                .Select(a => new EventApplicationToResponse()
                {
                    SearchPersonPanelId = a.SearchPersonPanelId,
                    UserId = a.UserId,
                    UserFirstName = a.User.FirstName,
                    UserLastName = a.User.LastName,
                    UserProfileImagePath = $"{serverUrl}/Images/UserProfiles/{a.User.ImagePath}",
                    UserHeadline = a.User.Headline,
                    DateOfCreation = a.DateOfCreation
                })
                .OrderByDescending(a => a.DateOfCreation)
                .ToListAsync();

            return applications;
        }
    }
}
