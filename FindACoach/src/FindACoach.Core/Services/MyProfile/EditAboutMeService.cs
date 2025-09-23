using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile;
using FindACoach.Core.ServiceContracts.MyProfile;

namespace FindACoach.Core.Services.MyProfile
{
    public class EditAboutMeService : IEditAboutMeService
    {
        private readonly IUsersRepository _usersRepository;

        public EditAboutMeService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task EditAboutMe(string userId, AboutMeDTO dto)
        {
            if (dto.AboutMe == null)
            {
                dto.AboutMe = String.Empty;
            }

            await _usersRepository.EditAboutMe(userId, dto);
        }
    }
}
