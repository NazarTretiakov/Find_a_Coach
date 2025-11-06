using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.IdentityEntities;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Forum;
using FindACoach.Core.Enums;
using FindACoach.Core.ServiceContracts.Network;
using FindACoach.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FindACoach.Infrastructure.Repositories
{
    public class EventApplicationsRepository : IEventApplicationsRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly INotificationsAdderService _notificationsAdderService;

        public EventApplicationsRepository(ApplicationDbContext db, IConfiguration configuration, INotificationsAdderService notificationsAdderService)
        {
            _db = db;
            _configuration = configuration;
            _notificationsAdderService = notificationsAdderService;
        }

        public async Task Add(EventApplication application)
        {
            if (await _db.EventApplications.FirstOrDefaultAsync(a => a.UserId == application.UserId && a.SearchPersonPanelId == application.SearchPersonPanelId) != null)
            {
                return;
            }

            await _db.EventApplications.AddAsync(application);

            await _db.SaveChangesAsync();

            application = await _db.EventApplications
                .Where(a => a.Id == application.Id)
                .Include(a => a.SearchPersonPanel)
                .ThenInclude(spp => spp.Event)
                .Include(a => a.User)
                .FirstAsync();

            await _notificationsAdderService.AddNotification(application.SearchPersonPanel.Event.UserId.ToString(),
                $"{application.User.FirstName} has applied on your event.",
                application.Id.ToString(),
                NotificationType.EventApplication);
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
