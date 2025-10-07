using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Forum;
using FindACoach.Core.DTO.MyProfile.Activities;
using FindACoach.Core.ServiceContracts.Forum.Activities;

namespace FindACoach.Core.Services.Forum.Activities
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

        public async Task<ActivityToResponse> GetActivity(string id)
        {
            return await _activitiesRepository.GetActivity(id);
        }

        public async Task<List<ActivityCardToResponse>> GetLastTwoActivities(string userId)
        {
            return await _activitiesRepository.GetLastTwoActivities(userId);
        }
    }
}
