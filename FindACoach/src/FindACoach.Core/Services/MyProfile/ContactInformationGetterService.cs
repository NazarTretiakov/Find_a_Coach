using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile;
using FindACoach.Core.ServiceContracts.MyProfile;

namespace FindACoach.Core.Services.MyProfile
{
    public class ContactInformationGetterService : IContactInformationGetterService
    {
        private readonly IUsersRepository _usersRepository;

        public ContactInformationGetterService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<ContactInformationToResponse> GetContactInformation(string userId)
        {
            return await _usersRepository.GetContactInformation(userId);
        }
    }
}
