using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Activities;
using FindACoach.Core.ServiceContracts.MyProfile.Activities;

namespace FindACoach.Core.Services.MyProfile.Activities
{
    public class ActivitiesGetterService : IActivitiesGetterService
    {
        private readonly IActivitiesRepository _activitiesRepository;

        public ActivitiesGetterService(IActivitiesRepository activitiesRepository)
        {
            _activitiesRepository = activitiesRepository;
        }

        public async Task<List<ActivityForActivitiesListToResponse>> GetActivitiesPaged(string userId, int page, int pageSize)
        {
            return await _activitiesRepository.GetActivitiesPaged(userId, page, pageSize);
        }

        public async Task<List<ActivityCardToResponse>> GetLastTwoActivities(string userId)
        {
            return await _activitiesRepository.GetLastTwoActivities(userId);
        }
    }
}
