using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO;
using FindACoach.Core.ServiceContracts.MyProfile;

namespace FindACoach.Core.Services.MyProfile
{
    public class EditPersonalInformationService : IEditPersonalInformationService
    {
        private readonly IUsersRepository _usersRepository;

        public EditPersonalInformationService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task EditPersonalInformation(string userId, EditPersonalInformationDTO dto)
        {
            await _usersRepository.EditPersonalInformation(userId, dto);
        }
    }
}
