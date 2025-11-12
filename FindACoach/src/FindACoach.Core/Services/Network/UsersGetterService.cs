using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Network;
using FindACoach.Core.ServiceContracts.Network;

namespace FindACoach.Core.Services.Network
{
    public class UsersGetterService : IUsersGetterService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersGetterService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<List<ConnectionToResponse>> GetFilteredUsers(string searchString, int page, int pageSize)
        {
            return await _usersRepository.GetFilteredUsers(searchString, page, pageSize);
        }

        public async Task<List<ConnectionToResponse>> GetRecommendedUsers(string userId, int page, int pageSize)
        {
            return await _usersRepository.GetRecommendedUsers(userId, page, pageSize);
        }
    }
}
