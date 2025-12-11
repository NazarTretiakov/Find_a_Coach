using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.DTO.Network;
using FindACoach.Core.Services.Network;
using Moq;

namespace UnitTests
{
    public class NetworkTests
    {
        [Fact]
        public async Task CheckUserUnreadNotifications_ShouldReturnTrue_WhenRepositoryReturnsTrue()
        {
            var userId = Guid.NewGuid().ToString();

            var repositoryMock = new Mock<INotificationsRepository>();

            repositoryMock
                .Setup(r => r.CheckUserUnreadNotifications(userId))
                .ReturnsAsync(true);

            var checkUnreadNotificationsService = new CheckUnreadNotificationsService(repositoryMock.Object);

            var result = await checkUnreadNotificationsService.CheckUserUnreadNotifications(userId);

            Assert.True(result);
        }

        [Fact]
        public async Task CheckUserUnreadNotifications_ShouldReturnFalse_WhenRepositoryReturnsFalse()
        {
            var userId = Guid.NewGuid().ToString();

            var repositoryMock = new Mock<INotificationsRepository>();

            repositoryMock
                .Setup(r => r.CheckUserUnreadNotifications(userId))
                .ReturnsAsync(false);

            var checkUnreadNotificationsService = new CheckUnreadNotificationsService(repositoryMock.Object);

            var result = await checkUnreadNotificationsService.CheckUserUnreadNotifications(userId);

            Assert.False(result);
        }
        [Fact]
        public async Task GetAllUserNotifications_ShouldReturnNotifications_WhenValidInput()
        {
            var userId = Guid.NewGuid().ToString();
            var page = 1;
            var pageSize = 7;

            var expectedNotifications = new List<NotificationToResponse>
            {
                new NotificationToResponse
                {
                    NotificationId = Guid.NewGuid(),
                    Content = "Content",
                    DateOfCreation = DateTime.Now.AddHours(-2),
                    Type = "Like",
                    NotifiedObjectId = Guid.NewGuid(),
                    RelatedUserFirstName = "Firstname",
                    RelatedUserLastName = "Lastname",
                    RelatedUserImagePath = "/images/users/john.png"
                },
                new NotificationToResponse
                {
                    NotificationId = Guid.NewGuid(),
                    Content = "Content",
                    DateOfCreation = DateTime.Now.AddHours(-1),
                    Type = "Comment",
                    NotifiedObjectId = Guid.NewGuid(),
                    RelatedUserFirstName = "Firstname",
                    RelatedUserLastName = "Lastname",
                    RelatedUserImagePath = "/images/users/emily.png"
                }
            };

            var repositoryMock = new Mock<INotificationsRepository>();

            repositoryMock
                .Setup(r => r.GetAllUserNotifications(userId, page, pageSize))
                .ReturnsAsync(expectedNotifications);

            var notificationsGetterService = new NotificationsGetterService(repositoryMock.Object);

            var result = await notificationsGetterService.GetAllUserNotifications(userId, page, pageSize);

            Assert.NotNull(result);
            Assert.Equal(expectedNotifications.Count, result.Count);
            Assert.Equal(expectedNotifications, result);
        }

        [Fact]
        public async Task AcceptConnectionRequest_ShouldCallService_WhenValidConnectionId()
        {
            var connectionId = Guid.NewGuid().ToString();
            var dto = new ConnectionRequestIdDTO
            {
                ConnectionId = connectionId
            };

            var repositoryMock = new Mock<IConnectionsRepository>();
            repositoryMock
                .Setup(r => r.AcceptConnectionRequest(connectionId))
                .Returns(Task.CompletedTask);

            var acceptConnectionRequestService = new AcceptConnectionRequestService(repositoryMock.Object);

            await acceptConnectionRequestService.AcceptConnectionRequest(dto);

            repositoryMock.Verify(
                r => r.AcceptConnectionRequest(connectionId),
                Times.Once
            );
        }
    }
}
