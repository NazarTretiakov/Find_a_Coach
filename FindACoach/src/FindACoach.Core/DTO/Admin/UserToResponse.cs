namespace FindACoach.Core.DTO.Admin
{
    public class UserToResponse
    {
        public Guid UserId { get; set; }
        public string ImagePath { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBlocked { get; set; }
    }
}
