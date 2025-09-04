using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.ServiceContracts.CompleteProfileWindow;
using Microsoft.IdentityModel.Tokens;

namespace FindACoach.Core.Services.CompleteProfileWindow
{
    public class ChangeStateService : IChangeStateService
    {
        private readonly IUsersRepository _usersRepository;

        public ChangeStateService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task ChangeState(string userId, bool isVisible, string title)
        {
            if (userId.IsNullOrEmpty() || isVisible == null || title.IsNullOrEmpty())
            {
                throw new ArgumentException("Arguments cannot be null or empty.");
            }

            await _usersRepository.ChangeCompleteProfileWindowState(userId, isVisible, title);
        }
    }
}
