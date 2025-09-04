using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO;
using FindACoach.Core.ServiceContracts.CompleteProfileWindow;
namespace FindACoach.Core.Services.CompleteProfileWindow
{
    public class GetStateService : IGetStateService
    {
        private readonly IUsersRepository _usersRepository;

        public GetStateService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<CompleteProfileWindowStateDTO> GetState(string userId)
        {
            return await _usersRepository.GetCompleteProfileWindowState(userId);
        }
    }
}
