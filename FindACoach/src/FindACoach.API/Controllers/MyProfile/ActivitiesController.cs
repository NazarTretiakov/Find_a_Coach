using FindACoach.Core.DTO.MyProfile.Activities;
using FindACoach.Core.ServiceContracts.Forum.Activities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FindACoach.API.Controllers.MyProfile
{
    public class ActivitiesController : CustomControllerBase
    {
        private readonly IAddActivityService _addActivityService;
        private readonly IActivitiesRemoverService _activitiesRemoverService;

        public ActivitiesController(IAddActivityService addActivityService, IActivitiesRemoverService activitiesRemoverService)
        {
            _addActivityService = addActivityService;
            _activitiesRemoverService = activitiesRemoverService;
        }

        [HttpPost("add")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddActivity([FromForm] AddActivityDTO dto)
        {
            if (ModelState.IsValid == false)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

                List<List<string>> skills = dto.SearchPersonPanels.Select(s => s.PreferredSkills.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                                                  .Select(skill => skill.Trim())
                                                                  .ToList())
                                                                  .ToList();

                foreach (List<string> panelSkills in skills)
                {
                    for (int i = 0; i < panelSkills.Count; i++)
                    {
                        if (panelSkills[i].Length > 30)
                        {
                            errorMessage += $" | Skill #{i} is too long. Maximum length is 30 characters.";
                        }
                    }
                    if (panelSkills.Count > 5)
                    {
                        errorMessage += $" | Too many preferred skills in search person panel. Maximum 5 preferred skills allowed.";
                    }
                }

                return BadRequest(new { errorMessage = errorMessage });
            }

            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _addActivityService.AddActivity(userId, dto);

            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteActivity([FromBody] DeleteActivityDTO dto)
        {
            await _activitiesRemoverService.DeleteActivity(dto.ActivityId, dto.UserId);

            return Ok();
        }
    }
}
