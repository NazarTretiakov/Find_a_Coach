using FindACoach.Core.DTO.MyProfile.Skills;
using FindACoach.Core.ServiceContracts.MyProfile.Skills;
using Microsoft.AspNetCore.Mvc;

namespace FindACoach.API.Controllers.MyProfile
{
    public class SkillsController: CustomControllerBase
    {
        private readonly ISkillsGetterService _skillsGetterService;

        public SkillsController(ISkillsGetterService skillsGetterService)
        {
            _skillsGetterService = skillsGetterService;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<SkillToResponse>>> GetAllSkills(string userId)
        {
            var skills = await _skillsGetterService.GetAllSkills(userId);

            return Ok(skills);
        }

        [HttpGet("get-two-skills-with-most-usages")]
        public async Task<ActionResult<List<SkillToResponse>>> GetTwoSkillsWithMostUsages(string userId)
        {
            var skills = await _skillsGetterService.GetTwoSkillsWithMostUsages(userId);

            return Ok(skills);
        }
    }
}