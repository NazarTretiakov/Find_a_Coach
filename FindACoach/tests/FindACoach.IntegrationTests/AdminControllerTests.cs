using FindACoach.Core.DTO.Admin;
using FindACoach.Core.DTO.MyProfile.Activities;
using FindACoach.Core.ServiceContracts.Admin;
using FindACoach.Core.ServiceContracts.Forum.Activities;
using FindACoach.Core.ServiceContracts.Network;
using FindACoach.IntegrationTests.FakeServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace FindACoach.IntegrationTests
{
    public class AdminControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _webApplicationFactory;

        public AdminControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _webApplicationFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = "TestScheme";
                        options.DefaultChallengeScheme = "TestScheme";
                    })
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestScheme", _ => { });

                    services.AddSingleton<IUsersGetterService, FakeUsersGetterService>();
                    services.AddSingleton<IToggleBlockOfUserService, FakeToggleBlockOfUserService>();
                    services.AddSingleton<IActivitiesGetterService, FakeActivitiesGetterService>();
                });
            });
        }

        private HttpClient GetClientAsAdmin()
        {
            var client = _webApplicationFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme", "Admin");

            return client;
        }

        private HttpClient GetClientAsUser()
        {
            var client = _webApplicationFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme", "User");

            return client;
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnsOk_WhenValidRequest()
        {
            var client = GetClientAsAdmin();
            var response = await client.GetAsync("api/admin/get-all-users?page=1&pageSize=7");


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var users = await response.Content.ReadFromJsonAsync<List<UserToResponse>>();
            Assert.NotNull(users);
            Assert.Equal(2, users.Count);
        }

        [Fact]
        public async Task GetFilteredUsers_ReturnsFilteredUsers_WhenValidRequest()
        {
            var client = GetClientAsAdmin();
            var response = await client.GetAsync("api/admin/get-filtered-users?searchString=John&page=1&pageSize=7");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var users = await response.Content.ReadFromJsonAsync<List<UserToResponse>>();
            Assert.Single(users);
            Assert.Equal("John", users[0].FirstName);
        }

        [Fact]
        public async Task ToggleBlockOfUser_ReturnsTrue_WhenValidRequest()
        {
            var client = GetClientAsAdmin();

            var dto = new UserIdDTO 
            {
                UserId = Guid.NewGuid().ToString()
            };

            var response = await client.PostAsJsonAsync("api/admin/toggle-block-of-user", dto);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var isBlocked = await response.Content.ReadFromJsonAsync<bool>();
            Assert.True(isBlocked);
        }

        [Fact]
        public async Task GetAllActivities_ReturnsActivities_WhenValidRequest()
        {
            var client = GetClientAsAdmin();

            var response = await client.GetAsync("api/admin/get-all-activities?page=1&pageSize=10");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var activities = await response.Content.ReadFromJsonAsync<List<ActivityForActivitiesListToResponse>>();
            Assert.NotNull(activities);
            Assert.Equal(2, activities.Count);
        }

        [Fact]
        public async Task OnlyAdminCanAccessController_ReturnsForbidden_WhenNonAdminTriesToUseController()
        {
            var client = GetClientAsUser();

            var endpoints = new string[]
            {
                "api/admin/get-all-users?page=1&pageSize=10",
                "api/admin/get-filtered-users?searchString=John&page=1&pageSize=7",
                "api/admin/get-all-activities?page=1&pageSize=7",
                "api/admin/toggle-block-of-user"
            };

            foreach (var endpoint in endpoints)
            {
                HttpResponseMessage response;

                if (endpoint == "api/admin/toggle-block-of-user")
                {
                    response = await client.PostAsJsonAsync(endpoint, new UserIdDTO { UserId = Guid.NewGuid().ToString() });
                }
                else
                {
                    response = await client.GetAsync(endpoint);
                }

                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            }
        }
    }
}
