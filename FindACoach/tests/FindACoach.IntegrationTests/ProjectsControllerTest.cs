using FindACoach.API.Controllers.MyProfile;
using FindACoach.Core.DTO.MyProfile.Projects;
using FindACoach.Core.ServiceContracts.MyProfile.Projects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace IntegrationTests
{
    public class ProjectsControllerTest
    {
        private readonly Mock<IProjectsAdderService> _projectsAdderServiceMocked;
        private readonly Mock<IProjectsGetterService> _projectsGetterServiceMocked;
        private readonly Mock<IProjectsEditorService> _projectsEditorServiceMocked;
        private readonly Mock<IProjectsRemoverService> _projectsRemoverServiceMocked;

        public ProjectsControllerTest()
        {
            _projectsAdderServiceMocked = new Mock<IProjectsAdderService>();
            _projectsGetterServiceMocked = new Mock<IProjectsGetterService>();
            _projectsEditorServiceMocked = new Mock<IProjectsEditorService>();
            _projectsRemoverServiceMocked = new Mock<IProjectsRemoverService>();
        }

        private ProjectsController CreateControllerWithUser(string userId)
        {
            var controller = new ProjectsController(_projectsAdderServiceMocked.Object, _projectsGetterServiceMocked.Object, _projectsEditorServiceMocked.Object, _projectsRemoverServiceMocked.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                { 
                    User = user 
                }
            };

            return controller;
        }

        [Fact]
        public async Task AddProject_ShouldCallService_AndReturnOk()
        {
            var userId = Guid.NewGuid().ToString();
            var dto = new AddProjectDTO 
            { 
                Title = "Test Project", 
                RelatedTo = "Test", 
                StartDate = DateTime.Now, 
                EndDate = DateTime.Now.AddDays(1) 
            };

            var controller = CreateControllerWithUser(userId);

            var result = await controller.AddProject(dto);

            Assert.IsType<OkResult>(result);

            _projectsAdderServiceMocked.Verify(
                s => s.AddProject(userId, dto), 
                Times.Once
            );
        }

        [Fact]
        public async Task GetAllProjects_ShouldReturnProjects()
        {
            var userId = Guid.NewGuid().ToString();
            var projects = new List<ProjectToResponse> 
            { 
                new ProjectToResponse 
                { 
                    ProjectId = Guid.NewGuid(),
                    Title = "Test"
                } 
            };

            _projectsGetterServiceMocked.Setup(s => s.GetAllProjects(userId)).ReturnsAsync(projects);
            var controller = CreateControllerWithUser(userId);

            var result = await controller.GetAllProjects(userId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var returnedProjects = Assert.IsType<List<ProjectToResponse>>(okResult.Value);

            Assert.Equal(projects.Count, returnedProjects.Count);
        }

        [Fact]
        public async Task GetLastTwoProjects_ShouldReturnProjects()
        {
            var userId = Guid.NewGuid().ToString();
            var projects = new List<ProjectToResponse> 
            { 
                new ProjectToResponse 
                { 
                    ProjectId = Guid.NewGuid(), 
                    Title = "Project1" 
                }, 
                new ProjectToResponse 
                { 
                    ProjectId = Guid.NewGuid(), 
                    Title = "Project2" 
                } 
            };

            _projectsGetterServiceMocked.Setup(s => s.GetLastTwoProjects(userId)).ReturnsAsync(projects);
            var controller = CreateControllerWithUser(userId);

            var result = await controller.GetLastTwoProjects(userId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var returnedProjects = Assert.IsType<List<ProjectToResponse>>(okResult.Value);

            Assert.Equal(2, returnedProjects.Count);
        }

        [Fact]
        public async Task GetProject_ShouldReturnProject()
        {
            var projectId = Guid.NewGuid();
            var project = new ProjectToResponse 
            { 
                ProjectId = projectId, 
                Title = "Project1" 
            };

            _projectsGetterServiceMocked.Setup(s => s.GetProject(projectId.ToString())).ReturnsAsync(project);
            var controller = CreateControllerWithUser("user123");

            var result = await controller.GetProject(projectId.ToString());

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var returnedProject = Assert.IsType<ProjectToResponse>(okResult.Value);

            Assert.Equal(projectId, returnedProject.ProjectId);
        }

        [Fact]
        public async Task EditProject_ShouldCallService_AndReturnOk()
        {
            var dto = new EditProjectDTO 
            { 
                ProjectId = Guid.NewGuid().ToString(), 
                Title = "Edited", 
                RelatedTo = "Test", 
                StartDate = DateTime.Now, 
                EndDate = DateTime.Now.AddDays(1) 
            };
            var controller = CreateControllerWithUser("user123");

            var result = await controller.EditProject(dto);

            var okResult = Assert.IsType<OkResult>(result);

            _projectsEditorServiceMocked.Verify(
                s => s.EditProject(dto), 
                Times.Once
            );
        }

        [Fact]
        public async Task DeleteProject_ShouldCallService_AndReturnOk()
        {
            var dto = new DeleteProjectDTO 
            { 
                ProjectId = Guid.NewGuid().ToString()
            };
            var controller = CreateControllerWithUser("user123");

            var result = await controller.DeleteProject(dto);

            var okResult = Assert.IsType<OkResult>(result);

            _projectsRemoverServiceMocked.Verify(
                s => s.DeleteProject(dto.ProjectId), 
                Times.Once
            );
        }
    }
}