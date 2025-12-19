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

        public async Task<ActivitiesPagedToResponse> GetActivitiesPaged(string userId, int page, int pageSize)
        {
            return await _activitiesRepository.GetActivitiesPaged(userId, page, pageSize);
        }

        public async Task<ActivitiesPagedToResponse> GetFilteredActivitiesPaged(string userId, int page, int pageSize, string searchString)
        {
            return await _activitiesRepository.GetFilteredActivitiesPaged(userId, 
                page, 
                pageSize,
                searchString
            );
        }

        public async Task<ActivitiesPagedToResponse> GetRecommendedActivitiesPaged(string userId, int page, int pageSize)
        {
            return await _activitiesRepository.GetRecommendedActivitiesPaged(userId, page, pageSize);
        }

        public async Task<ActivitiesPagedToResponse> GetFilteredRecommendedActivitiesPaged(int page, int pageSize, string searchString)
        {
            return await _activitiesRepository.GetFilteredRecommendedActivitiesPaged(
                page,
                pageSize,
                searchString
            );
        }

        public async Task<ActivityToResponse> GetActivity(string id)
        {
            return await _activitiesRepository.GetActivity(id);
        }

        public async Task<List<ActivityCardToResponse>> GetLastTwoActivities(string userId)
        {
            return await _activitiesRepository.GetLastTwoActivities(userId);
        }

        public async Task<ActivitiesPagedToResponse> GetAllActivities(int page, int pageSize)
        {
            return await _activitiesRepository.GetAllActivities(page, pageSize);
        }

        public async Task<ActivitiesPagedToResponse> GetSavedActivitiesPaged(string userId, int page, int pageSize)
        {
            return await _activitiesRepository.GetSavedActivitiesPaged(userId, page, pageSize);
        }
    }
}
