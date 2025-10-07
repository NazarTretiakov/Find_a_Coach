using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.ServiceContracts.Forum.Activities;

namespace FindACoach.Core.Services.Forum.Activities
{
    public class ActivitiesRemoverService : IActivitiesRemoverService
    {
        private readonly IActivitiesRepository _activitysRepository;

        public ActivitiesRemoverService(IActivitiesRepository activitysRepository)
        {
            _activitysRepository = activitysRepository;
        }

        public async Task DeleteActivity(string activityId, string userId)
        {
            await _activitysRepository.DeleteActivity(activityId, userId);
        }
    }
}
