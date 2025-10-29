using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Forum;
using FindACoach.Core.ServiceContracts.Forum.Activities;

namespace FindACoach.Core.Services.Forum.Activities
{
    public class EventApplicationsGetterService : IEventApplicationsGetterService
    {
        private readonly IEventApplicationsRepository _eventApplicationsRepository;

        public EventApplicationsGetterService(IEventApplicationsRepository eventApplicationsRepository)
        {
            _eventApplicationsRepository = eventApplicationsRepository;
        }

        public async Task<List<EventApplicationToResponse>> GetPanelApplications(string searchPersonPanelId)
        {
            return await _eventApplicationsRepository.GetApplications(searchPersonPanelId);
        }
    }
}
