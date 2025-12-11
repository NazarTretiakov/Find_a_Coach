using FindACoach.Core.DTO.Forum;
using FindACoach.Core.DTO.MyProfile.Activities;
using FindACoach.Core.ServiceContracts.Forum.Activities;

namespace FindACoach.IntegrationTests.FakeServices
{
    public class FakeActivitiesGetterService: IActivitiesGetterService
    {
        public Task<List<ActivityForActivitiesListToResponse>> GetActivitiesPaged(string userId, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<ActivityToResponse> GetActivity(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ActivityForActivitiesListToResponse>> GetAllActivities(int page, int pageSize)
        {
            return Task.FromResult(new List<ActivityForActivitiesListToResponse>
            {
                new ActivityForActivitiesListToResponse()
                { 
                    Id = Guid.NewGuid(), 
                    Title = "Activity 1" 
                },
                new ActivityForActivitiesListToResponse()
                { 
                    Id = Guid.NewGuid(), 
                    Title = "Activity 2" 
                }
            });
        }

        public Task<List<ActivityForActivitiesListToResponse>> GetFilteredActivitiesPaged(string userId, int page, int pageSize, string searchString)
        {
            throw new NotImplementedException();
        }

        public Task<List<ActivityForActivitiesListToResponse>> GetFilteredRecommendedActivitiesPaged(int page, int pageSize, string searchString)
        {
            throw new NotImplementedException();
        }

        public Task<List<ActivityCardToResponse>> GetLastTwoActivities(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ActivityForActivitiesListToResponse>> GetRecommendedActivitiesPaged(string userId, int page, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
