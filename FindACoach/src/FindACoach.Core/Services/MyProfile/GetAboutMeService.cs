using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile;
using FindACoach.Core.ServiceContracts.MyProfile;

namespace FindACoach.Core.Services.MyProfile
{
    public class GetAboutMeService : IGetAboutMeService
    {
        private readonly IUsersRepository _usersRepository;

        public GetAboutMeService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<AboutMeDTO> GetAboutMe(string userId)
        {
            return await _usersRepository.GetAboutMe(userId);
        }
    }
}
