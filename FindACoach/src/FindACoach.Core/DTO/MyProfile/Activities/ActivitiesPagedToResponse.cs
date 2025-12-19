namespace FindACoach.Core.DTO.MyProfile.Activities
{
    public class ActivitiesPagedToResponse
    {
        public List<ActivityForActivitiesListToResponse> Activities { get; set; }
        public bool IsMoreActivitiesLeft { get; set; }
    }
}
