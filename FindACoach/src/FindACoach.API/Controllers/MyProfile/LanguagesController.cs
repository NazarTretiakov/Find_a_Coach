using FindACoach.Core.DTO.MyProfile.Languages;
using FindACoach.Core.ServiceContracts.MyProfile.Languages;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FindACoach.API.Controllers.MyProfile
{
    public class LanguagesController : CustomControllerBase
    {
        private readonly ILanguagesAdderService _languagesAdderService;
        private readonly ILanguagesGetterService _languagesGetterService;
        private readonly ILanguagesEditorService _languagesEditorService;
        private readonly ILanguagesRemoverService _languagesRemoverService;

        public LanguagesController(ILanguagesAdderService languagesAdderService, ILanguagesGetterService languagesGetterService, ILanguagesEditorService languagesEditorService, ILanguagesRemoverService languagesRemoverService)
        {
            _languagesAdderService = languagesAdderService;
            _languagesGetterService = languagesGetterService;
            _languagesEditorService = languagesEditorService;
            _languagesRemoverService = languagesRemoverService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddLanguage([FromBody] AddLanguageDTO dto)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _languagesAdderService.AddLanguage(userId, dto);

            return Ok();
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<LanguageToResponse>>> GetAllLanguages(string userId)
        {
            var languages = await _languagesGetterService.GetAllLanguages(userId);

            return Ok(languages);
        }

        [HttpGet("get-two")]
        public async Task<ActionResult<List<LanguageToResponse>>> GetTwoLanguages(string userId)
        {
            var languages = await _languagesGetterService.GetTwoLanguages(userId);

            return Ok(languages);
        }

        [HttpGet("get")]
        public async Task<ActionResult<LanguageToResponse>> GetLanguage(string languageId)
        {
            var language = await _languagesGetterService.GetLanguage(languageId);

            return Ok(language);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditLanguage([FromBody] EditLanguageDTO dto)
        {
            await _languagesEditorService.EditLanguage(dto);

            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteLanguage([FromBody] DeleteLanguageDTO dto)
        {
            await _languagesRemoverService.DeleteLanguage(dto.LanguageId);

            return Ok();
        }
    }
}
