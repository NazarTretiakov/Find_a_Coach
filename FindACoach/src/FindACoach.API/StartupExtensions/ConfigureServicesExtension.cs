using FindACoach.Core.Configuration;
using FindACoach.Core.Domain.IdentityEntities;
using FindACoach.Core.Domain.RepositoryContracts;
using FindACoach.Core.ServiceContracts;
using FindACoach.Core.ServiceContracts.Authentication;
using FindACoach.Core.ServiceContracts.CompleteProfileWindow;
using FindACoach.Core.ServiceContracts.Forum.Activities;
using FindACoach.Core.ServiceContracts.MyProfile;
using FindACoach.Core.ServiceContracts.MyProfile.Certifications;
using FindACoach.Core.ServiceContracts.MyProfile.Education;
using FindACoach.Core.ServiceContracts.MyProfile.Experience;
using FindACoach.Core.ServiceContracts.MyProfile.Languages;
using FindACoach.Core.ServiceContracts.MyProfile.Projects;
using FindACoach.Core.ServiceContracts.MyProfile.Skills;
using FindACoach.Core.Services;
using FindACoach.Core.Services.Authentication;
using FindACoach.Core.Services.CompleteProfileWindow;
using FindACoach.Core.Services.Forum.Activities;
using FindACoach.Core.Services.MyProfile;
using FindACoach.Core.Services.MyProfile.Certifications;
using FindACoach.Core.Services.MyProfile.Education;
using FindACoach.Core.Services.MyProfile.Experience;
using FindACoach.Core.Services.MyProfile.Languages;
using FindACoach.Core.Services.MyProfile.Projects;
using FindACoach.Infrastructure.DbContext;
using FindACoach.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace FindACoach.API.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
                options.Filters.Add(new ConsumesAttribute("application/json"));

                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
              .AddXmlSerializerFormatters();


            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<IGetStateService, GetStateService>();
            services.AddScoped<IChangeStateService, ChangeStateService>();
            services.AddScoped<IEditPersonalInformationService, EditPersonalInformationService>();
            services.AddScoped<IGetPersonalInformationService, GetPersonalInformationService>();
            services.AddScoped<IGetPersonalAndContactInformationService, GetPersonalAndContactInformationService>();
            services.AddScoped<IEditAboutMeService, EditAboutMeService>();
            services.AddScoped<IGetAboutMeService, GetAboutMeService>();
            services.AddScoped<IPositionsAdderService, PositionsAdderService>();
            services.AddScoped<IPositionsGetterService, PositionsGetterService>();
            services.AddScoped<IPositionsEditorService, PositionsEditorService>();
            services.AddScoped<IPositionsRemoverService, PositionsRemoverService>();
            services.AddScoped<ISchoolsAdderService, SchoolsAdderService>();
            services.AddScoped<ISchoolsGetterService, SchoolsGetterService>();
            services.AddScoped<ISchoolsEditorService, SchoolsEditorService>();
            services.AddScoped<ISchoolsRemoverService, SchoolsRemoverService>();
            services.AddScoped<IProjectsAdderService, ProjectsAdderService>();
            services.AddScoped<IProjectsGetterService, ProjectsGetterService>();
            services.AddScoped<IProjectsEditorService, ProjectsEditorService>();
            services.AddScoped<IProjectsRemoverService, ProjectsRemoverService>();
            services.AddScoped<ICertificationsAdderService, CertificationsAdderService>();
            services.AddScoped<ICertificationsGetterService, CertificationsGetterService>();
            services.AddScoped<ICertificationsEditorService, CertificationsEditorService>();
            services.AddScoped<ICertificationsRemoverService, CertificationsRemoverService>();
            services.AddScoped<ISkillsGetterService, SkillsGetterService>();
            services.AddScoped<ILanguagesAdderService, LanguagesAdderService>();
            services.AddScoped<ILanguagesGetterService, LanguagesGetterService>();
            services.AddScoped<ILanguagesEditorService, LanguagesEditorService>();
            services.AddScoped<ILanguagesRemoverService, LanguagesRemoverService>();
            services.AddScoped<IAddActivityService, AddActivityService>();
            services.AddScoped<IActivitiesGetterService, ActivitiesGetterService>();
            services.AddScoped<IToggleLikeService, ToggleLikeService>();
            services.AddScoped<IToggleSaveService, ToggleSaveService>();
            services.AddScoped<ICommentsAdderService, CommentsAdderService>();
            services.AddScoped<ICommentsGetterService, CommentsGetterService>();
            services.AddScoped<ICommentsRemoverService, CommentsRemoverService>();
            services.AddScoped<IActivitiesRemoverService, ActivitiesRemoverService>();

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IPositionsRepository, PositionsRepository>();
            services.AddScoped<ISchoolsRepository, SchoolsRepository>();
            services.AddScoped<IProjectsRepository, ProjectsRepository>();
            services.AddScoped<ICertificationsRepository, CertificationsRepository>();
            services.AddScoped<ILanguagesRepository, LanguagesRepository>();
            services.AddScoped<ISkillsRepository, SkillsRepository>();
            services.AddScoped<IActivitiesRepository, ActivitiesRepository>();
            services.AddScoped<ILikesRepository, LikesRepository>();
            services.AddScoped<ISavesRepository, SavesRepository>();
            services.AddScoped<ICommentsRepository, CommentsRepository>();


            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policyBuilder =>
                {
                    policyBuilder.WithOrigins(configuration.GetValue<string>("ClientUrl"))
                                 .WithHeaders("Authorization", "origin", "accept", "content-type")
                                 .WithMethods("GET", "POST", "PUT", "DELETE");
                });
            });

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders()
              .AddUserStore<UserStore<User, Role, ApplicationDbContext, Guid>>()
              .AddRoleStore<RoleStore<Role, ApplicationDbContext, Guid>>();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ValidateLifetime = true,
                    NameClaimType = ClaimTypes.Email,
                    RoleClaimType = ClaimTypes.Role
                };
            });

            services.AddAuthorization();

            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}