using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile;
using FindACoach.Core.ServiceContracts.MyProfile;

namespace FindACoach.Core.Services.MyProfile
{
    public class GetPersonalAndContactInformationService : IGetPersonalAndContactInformationService
    {
        private readonly IUsersRepository _usersRepository;

        public GetPersonalAndContactInformationService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public async Task<PersonalAndContactInformationToResponse> GetPersonalAndContactInformation(string userId)
        {
            return await _usersRepository.GetPersonalAndContactInformation(userId);
        }
    }
}
