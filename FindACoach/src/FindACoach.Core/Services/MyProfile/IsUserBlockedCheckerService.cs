using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.ServiceContracts.MyProfile;

namespace FindACoach.Core.Services.MyProfile
{
    public class IsUserBlockedCheckerService : IIsUserBlockedCheckerService
    {
        private readonly IUsersRepository _usersRepository;

        public IsUserBlockedCheckerService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<bool> IsUserBlocked(string userId)
        {
            return await _usersRepository.IsUserBlocked(userId);
        }
    }
}
