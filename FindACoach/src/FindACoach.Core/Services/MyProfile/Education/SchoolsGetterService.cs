using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Education;
using FindACoach.Core.ServiceContracts.MyProfile.Education;

namespace FindACoach.Core.Services.MyProfile.Education
{
    public class SchoolsGetterService : ISchoolsGetterService
    {
        private readonly ISchoolsRepository _schoolsRepository;

        public SchoolsGetterService(ISchoolsRepository schoolsRepository)
        {
            _schoolsRepository = schoolsRepository;
        }
        public async Task<List<SchoolToResponse>> GetAllSchools(string userId)
        {
            return await _schoolsRepository.GetAllSchools(userId);
        }

        public async Task<List<SchoolToResponse>> GetLastTwoSchools(string userId)
        {
            return await _schoolsRepository.GetLastTwoSchools(userId);
        }

        public async Task<SchoolToResponse> GetSchool(string schoolId)
        {
            return await _schoolsRepository.GetSchool(schoolId);
        }
    }
}
