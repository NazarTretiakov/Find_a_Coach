using FindACoach.Core.DTO.Admin;
using FindACoach.Core.DTO.Network;
using FindACoach.Core.ServiceContracts.Network;

namespace FindACoach.IntegrationTests.FakeServices
{
    public class FakeUsersGetterService : IUsersGetterService
    {
        public Task<List<UserToResponse>> GetAllUsers(int page, int pageSize)
        {
            return Task.FromResult(new List<UserToResponse>
            {
                new UserToResponse()
                { 
                    UserId = Guid.NewGuid(), 
                    FirstName = "John", 
                    LastName = "Doe" 
                },
                new UserToResponse() 
                { 
                    UserId = Guid.NewGuid(), 
                    FirstName = "Jane",
                    LastName = "Doe"
                }
            });
        }

        public Task<List<ConnectionToResponse>> GetRecommendedUsers(string userId, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        Task<List<ConnectionToResponse>> IUsersGetterService.GetFilteredUsers(string searchString, int page, int pageSize)
        {
            var users = new List<ConnectionToResponse>
            {
                new ConnectionToResponse()
                {
                    FirstName = "John",
                    LastName = "Doe"
                },
                new ConnectionToResponse()
                {
                    FirstName = "Jane",
                    LastName = "Doe"
                }
            };

            return Task.FromResult(users.Where(u => (u.FirstName + " " + u.LastName).Contains(searchString)).ToList());
        }
    }
}
