using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Skills;
using FindACoach.Core.ServiceContracts.MyProfile.Skills;

namespace FindACoach.Core.Services.MyProfile.Education
{
    public class SkillsGetterService : ISkillsGetterService
    {
        private readonly ISkillsRepository _skillsRepository;

        public SkillsGetterService(ISkillsRepository skillsRepository)
        {
            _skillsRepository = skillsRepository;
        }
        public async Task<List<SkillToResponse>> GetAllSkills(string userId)
        {
            return await _skillsRepository.GetAllSkills(userId);
        }

        public async Task<List<SkillToResponse>> GetTwoSkillsWithMostUsages(string userId)
        {
            return await _skillsRepository.GetTwoSkillsWithMostUsages(userId);
        }
    }
}
