using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Education;
using FindACoach.Core.ServiceContracts.MyProfile.Education;

namespace FindACoach.Core.Services.MyProfile.Education
{
    public class SchoolsAdderService : ISchoolsAdderService
    {
        private readonly ISchoolsRepository _schoolsRepository;

        public SchoolsAdderService(ISchoolsRepository schoolsRepository)
        {
            _schoolsRepository = schoolsRepository;
        }

        public async Task AddSchool(string userId, AddSchoolDTO dto)
        {
            await _schoolsRepository.AddSchool(userId, dto);
        }
    }
}
