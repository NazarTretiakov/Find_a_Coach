using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace FindACoach.Infrastructure.Repositories
{
    public class VotesRepository : IVotesRepository
    {
        private readonly ApplicationDbContext _db;

        public VotesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Vote>> Add(Vote vote)
        {
            _db.Votes.Add(vote);

            _db.SaveChanges();

            Survey survey = await _db.Surveys
                .Where(s => s.Options.Any(o => o.Id == vote.SurveyOptionId))
                .Select(s => new Survey
                {
                    Id = s.Id,
                    Options = s.Options
                        .Select(o => new SurveyOption
                        {
                            Id = o.Id,
                            Votes = o.Votes
                                .Select(v => new Vote
                                {
                                    Id = v.Id,
                                    UserId = v.UserId,
                                    SurveyOptionId = v.SurveyOptionId
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            List<Vote> allVotes = new List<Vote>();

            foreach (var option in survey.Options)
            {
                allVotes.AddRange(option.Votes);
            }

            return allVotes;
        }

        public async Task<List<Vote>> GetSurveyVotes(string surveyId)
        {
            Survey survey = await _db.Surveys
                .Where(s => s.Id == Guid.Parse(surveyId))
                .Select(s => new Survey
                {
                    Id = s.Id,
                    Options = s.Options
                        .Select(o => new SurveyOption
                        {
                            Id = o.Id,
                            Votes = o.Votes
                                .Select(v => new Vote
                                {
                                    Id = v.Id,
                                    UserId = v.UserId,
                                    SurveyOptionId = v.SurveyOptionId
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            List<Vote> allVotes = new List<Vote>();

            foreach (var option in survey.Options)
            {
                allVotes.AddRange(option.Votes);
            }

            return allVotes;
        }
    }
}
