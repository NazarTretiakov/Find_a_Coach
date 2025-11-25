using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.ServiceContracts.Admin;

namespace FindACoach.Core.Services.Admin
{
    public class ToggleBlockOfUserService : IToggleBlockOfUserService
    {
        private readonly IUsersRepository _usersRepository;

        public ToggleBlockOfUserService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<bool> Toggle(string userId)
        {
            return await _usersRepository.ToggleBlock(userId);
        }
    }
}
