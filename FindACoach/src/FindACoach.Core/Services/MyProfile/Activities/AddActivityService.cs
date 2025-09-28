using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.MyProfile.Activities;
using FindACoach.Core.ServiceContracts.MyProfile.Activities;

namespace FindACoach.Core.Services.MyProfile.Activities
{
    public class AddActivityService : IAddActivityService
    {
        private readonly IActivitiesRepository _activitiesRepository;

        public AddActivityService(IActivitiesRepository activitiesRepository)
        {
            _activitiesRepository = activitiesRepository;
        }

        public async Task AddActivity(string userId, AddActivityDTO dto)
        {
            if (dto.ActivityType == Enums.ActivityType.Event)
            {
                if (dto.BeginningDate == null)
                {
                    throw new ArgumentException(message: "Argument \"Date of beginning\" can not be null.");
                }

                EventDTO eventDTO = new EventDTO()
                {
                    Title = dto.Title,
                    BeginningDate = dto.BeginningDate,
                    Image = dto.Image,
                    Description = dto.Description,
                    Subjects = dto.Subjects,
                    SearchPersonPanels = dto.SearchPersonPanels
                };
                await _activitiesRepository.AddEvent(userId, eventDTO);
            }
            else if (dto.ActivityType == Enums.ActivityType.Survey)
            {
                if (dto.SurveyOptions == null)
                {
                    throw new ArgumentException(message: "Argument \"Survey options\" can not be null.");
                }

                SurveyDTO surveyDTO = new SurveyDTO()
                {
                    Title = dto.Title,
                    Image = dto.Image,
                    Description = dto.Description,
                    Subjects = dto.Subjects,
                    SurveyOptions = dto.SurveyOptions
                };
                await _activitiesRepository.AddSurvey(userId, surveyDTO);
            }
            else if (dto.ActivityType == Enums.ActivityType.QA)
            {
                QADTO QADTO = new QADTO()
                {
                    Title = dto.Title,
                    Image = dto.Image,
                    Description = dto.Description,
                    Subjects = dto.Subjects
                };
                await _activitiesRepository.AddQA(userId, QADTO);
            }
            else
            {
                PostDTO PostDTO = new PostDTO()
                {
                    Title = dto.Title,
                    Image = dto.Image,
                    Description = dto.Description,
                    Subjects = dto.Subjects
                };
                await _activitiesRepository.AddPost(userId, PostDTO);
            }
        }
    }
}
