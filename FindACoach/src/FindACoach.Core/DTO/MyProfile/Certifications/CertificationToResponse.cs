namespace FindACoach.Core.DTO.MyProfile.Certifications
{
    public class CertificationToResponse
    {
        public string CertificationId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string IssuingOrganization { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public string? ImagePath { get; set; }
        public string CredentialId { get; set; } = string.Empty;
        public string CredentialUrl { get; set; } = string.Empty;
        public List<string> SkillTitles { get; set; } = new List<string>();
    }
}