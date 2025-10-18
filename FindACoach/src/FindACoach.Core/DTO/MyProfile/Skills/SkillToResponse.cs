namespace FindACoach.Core.DTO.MyProfile.Skills
{
    public class SkillToResponse
    {
        public string SkillId { get; set; } = string.Empty;
        //public string UserId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public List<UsageOfSkill> Usages { get; set; } = new List<UsageOfSkill>();
    }

    public class UsageOfSkill
    {
        public string Title { get; set; }
        public string Type { get; set; }
    }
}
