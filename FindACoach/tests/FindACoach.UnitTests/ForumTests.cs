using FindACoach.Core.Domain.Entities.Activity;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Forum;
using FindACoach.Core.DTO.MyProfile.Activities;
using FindACoach.Core.Services.Forum.Activities;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace UnitTests
{
    public class ForumTests
    {
        [Fact]
        public async void GetRecommendedActivitiesPaged_ShouldReturnPagedActivities_WhenValidInput()
        {
            var userId = Guid.NewGuid().ToString();
            var page = 1;
            var pageSize = 7;

            var expectedActivities = new List<ActivityForActivitiesListToResponse>
            {
                new ActivityForActivitiesListToResponse 
                {
                    Id = Guid.NewGuid(),
                    ImagePathOfCreator = "/images/users/u1.png",
                    FirstNameOfCreator = "Firstname",
                    LastNameOfCreator = "Lastname",
                    PublicationDate = DateTime.Now.AddDays(-2),
                    Title = "Some title",
                    Subjects = new List<string> { "subject 1" },
                    Description = "Some description",
                    ActivityType = "Post"
                },
                new ActivityForActivitiesListToResponse
                {
                    Id = Guid.NewGuid(),
                    ImagePathOfCreator = "/images/users/u1.png",
                    FirstNameOfCreator = "Firstname",
                    LastNameOfCreator = "Lastname",
                    PublicationDate = DateTime.Now.AddDays(-2),
                    Title = "Some title",
                    Subjects = new List<string> { "subject 1" },
                    Description = "Some description",
                    ActivityType = "QA"
                }
            };

            var repositoryMocked = new Mock<IActivitiesRepository>();

            repositoryMocked.Setup(r => r.GetRecommendedActivitiesPaged(userId, page, pageSize))
                            .ReturnsAsync(expectedActivities);

            var activitiesGetterService = new ActivitiesGetterService(repositoryMocked.Object);

            var result = await activitiesGetterService.GetRecommendedActivitiesPaged(userId, page, pageSize);

            Assert.NotNull(result);
            Assert.Equal(expectedActivities.Count, result.Count);
            Assert.Equal(expectedActivities, result);
        }

        [Fact]
        public async void GetActivity_ShouldReturnActivityTypeOfEvent_WhenValidInput()
        {
            var activityId = Guid.NewGuid().ToString();

            var expectedActivity = new EventToResponse()
            {
                Id = Guid.NewGuid(),
                ImagePath = "/images/users/u1.png",
                UserFirstName = "Firstname",
                UserLastName = "Lastname",
                BeginningDate = DateTime.Now.AddDays(-2),
                CreatedAt = DateTime.Now.AddDays(-10),
                Title = "Some title",
                Subjects = new List<string> { "subject 1" },
                Description = "Some description",
                SearchPersonPanels = new List<SearchPersonPanelToResponse>()
                {
                    new SearchPersonPanelToResponse
                    {
                        Id = Guid.NewGuid(),
                        PositionName = "PanelFirstName",
                        Description = "Description",
                        PreferredSkills = new List<string> { "skill1", "skill2" }
                    }
                }
            };

            var repositoryMocked = new Mock<IActivitiesRepository>();

            repositoryMocked.Setup(r => r.GetActivity(activityId))
                            .ReturnsAsync(expectedActivity);

            var activitiesGetterService = new ActivitiesGetterService(repositoryMocked.Object);

            var result = await activitiesGetterService.GetActivity(activityId);

            Assert.NotNull(result);
            Assert.IsType(typeof(EventToResponse), result);
            Assert.Equal(expectedActivity, result);
        }

        [Fact]
        public async Task ApplyOnEvent_ShouldCreateApplication_WhenUserIdMatches()
        {
            var repositoryMock = new Mock<IEventApplicationsRepository>();

            var userId = Guid.NewGuid().ToString();
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }, "TestAuth");

            var user = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext();
            httpContext.User = user;

            var httpAccessorMock = new Mock<IHttpContextAccessor>();
            httpAccessorMock.Setup(h => h.HttpContext).Returns(httpContext);

            var eventApplicationsCreatorService = new EventApplicationsCreatorService(repositoryMock.Object, httpAccessorMock.Object);

            var dto = new ApplyOnEventDTO
            {

                SearchPersonPanelId = Guid.NewGuid().ToString(),
                UserId = userId
            };

            await eventApplicationsCreatorService.ApplyOnEvent(dto);

            repositoryMock.Verify(
                r => r.Add(It.IsAny<EventApplication>()),
                Times.Once
            );
        }
    }
}