using FindACoach.Core.ServiceContracts.Admin;

namespace FindACoach.IntegrationTests.FakeServices
{
    public class FakeToggleBlockOfUserService : IToggleBlockOfUserService
    {
        public Task<bool> Toggle(string userId)
        {
            return Task.FromResult(true);
        }
    }
}
