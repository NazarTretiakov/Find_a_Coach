using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Forum;
using FindACoach.Core.DTO.MyProfile.Activities;
using FindACoach.Core.ServiceContracts.Forum.Activities;

namespace FindACoach.Core.Services.Forum.Activities
{
    public class QAAnswersGetterService : IQAAnswersGetterService
    {
        private readonly IQAAnswersRepository _QAAnswersRepository;

        public QAAnswersGetterService(IQAAnswersRepository QAAnswersRepository)
        {
            _QAAnswersRepository = QAAnswersRepository;
        }

        public async Task<List<QAAnswerToResponse>> GetQAAnswers(string QAId)
        {
            return await _QAAnswersRepository.GetQAAnswers(QAId);
        }
    }
}
