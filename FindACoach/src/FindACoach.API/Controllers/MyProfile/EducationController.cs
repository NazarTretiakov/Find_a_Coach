using FindACoach.Core.DTO.MyProfile.Education;
using FindACoach.Core.ServiceContracts.MyProfile.Education;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FindACoach.API.Controllers.MyProfile
{
    public class EducationController : CustomControllerBase
    {
        private readonly ISchoolsAdderService _schoolsAdderService;
        private readonly ISchoolsGetterService _schoolsGetterService;
        private readonly ISchoolsEditorService _schoolsEditorService;
        private readonly ISchoolsRemoverService _schoolsRemoverService; 

        public EducationController(ISchoolsAdderService schoolsAdderService, ISchoolsGetterService schoolsGetterService, ISchoolsEditorService schoolsEditorService, ISchoolsRemoverService schoolsRemoverService)
        {
            _schoolsAdderService = schoolsAdderService;
            _schoolsGetterService = schoolsGetterService;
            _schoolsEditorService = schoolsEditorService;
            _schoolsRemoverService = schoolsRemoverService;
        }

        [HttpPost("add-school")]
        public async Task<IActionResult> AddSchool([FromBody] AddSchoolDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _schoolsAdderService.AddSchool(userId, dto);

            return Ok();
        }

        [HttpGet("get-all-schools")]
        public async Task<ActionResult<List<SchoolToResponse>>> GetAllSchools(string userId)
        {
            var schools = await _schoolsGetterService.GetAllSchools(userId);

            return Ok(schools);
        }

        [HttpGet("get-last-two-schools")]
        public async Task<ActionResult<List<SchoolToResponse>>> GetLastTwoSchools(string userId)
        {
            var schools = await _schoolsGetterService.GetLastTwoSchools(userId);

            return Ok(schools);
        }

        [HttpGet("get-school")]
        public async Task<ActionResult<SchoolToResponse>> GetSchool(string schoolId)
        {
            var school = await _schoolsGetterService.GetSchool(schoolId);

            return Ok(school);
        }

        [HttpPost("edit-school")]
        public async Task<IActionResult> EditSchool([FromBody] EditSchoolDTO dto)
        {
            await _schoolsEditorService.EditSchool(dto);

            return Ok();
        }

        [HttpPost("delete-school")]
        public async Task<IActionResult> DeleteSchool([FromBody] DeleteSchoolDTO dto)
        {
            await _schoolsRemoverService.DeleteSchool(dto.SchoolId);

            return Ok();
        }
    }
}