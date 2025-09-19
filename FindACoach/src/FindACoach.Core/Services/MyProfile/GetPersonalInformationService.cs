using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile;
using FindACoach.Core.ServiceContracts.MyProfile;

namespace FindACoach.Core.Services.MyProfile
{
    public class GetPersonalInformationService : IGetPersonalInformationService
    {
        private readonly IUsersRepository _usersRepository;

        public GetPersonalInformationService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public async Task<PersonalInformationToResponse> GetPersonalInformation(string userId)
        {
            return await _usersRepository.GetPersonalInformation(userId);
        }
    }
}
