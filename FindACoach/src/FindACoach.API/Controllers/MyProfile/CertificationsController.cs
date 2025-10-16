using FindACoach.Core.DTO.MyProfile.Certifications;
using FindACoach.Core.DTO.MyProfile.Education;
using FindACoach.Core.ServiceContracts.MyProfile.Certifications;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FindACoach.API.Controllers.MyProfile
{
    public class CertificationsController: CustomControllerBase
    {
        private readonly ICertificationsAdderService _certificationsAdderService;
        private readonly ICertificationsGetterService _certificationsGetterService;
        private readonly ICertificationsEditorService _certificationsEditorService;
        private readonly ICertificationsRemoverService _certificationsRemoverService;

        public CertificationsController(ICertificationsAdderService certificationsAdderService, ICertificationsGetterService certificationsGetterService, ICertificationsEditorService certificationsEditorService, ICertificationsRemoverService certificationsRemoverService)
        {
            _certificationsAdderService = certificationsAdderService;
            _certificationsGetterService = certificationsGetterService;
            _certificationsEditorService= certificationsEditorService;
            _certificationsRemoverService = certificationsRemoverService;
        }

        [HttpPost("add")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddCertification([FromForm] AddCertificationDTO dto)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _certificationsAdderService.AddCertification(userId, dto);

            return Ok();
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<CertificationToResponse>>> GetAllCertifications(string userId)
        {
            var certifications = await _certificationsGetterService.GetAllCertifications(userId);

            return Ok(certifications);
        }

        [HttpGet("get-last-two")]
        public async Task<ActionResult<List<CertificationToResponse>>> GetLastTwoCertifications(string userId)
        {
            var certifications = await _certificationsGetterService.GetLastTwoCertifications(userId);

            return Ok(certifications);
        }

        [HttpGet("get")]
        public async Task<ActionResult<CertificationToResponse>> GetCertification(string certificationId)
        {
            var certification = await _certificationsGetterService.GetCertification(certificationId);

            return Ok(certification);
        }

        [HttpPost("edit")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> EditCertification([FromForm] EditCertificationDTO dto)
        {
            await _certificationsEditorService.EditCertification(dto);

            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteCertification([FromBody] DeleteCertificationDTO dto)
        {
            await _certificationsRemoverService.DeleteCertification(dto.CertificationId);

            return Ok();
        }
    }
}
