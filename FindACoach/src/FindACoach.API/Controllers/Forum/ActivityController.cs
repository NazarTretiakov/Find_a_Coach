using FindACoach.Core.DTO.Forum;
using FindACoach.Core.ServiceContracts.Forum.Activities;
using Microsoft.AspNetCore.Mvc;

namespace FindACoach.API.Controllers.Forum
{
    public class ActivityController : CustomControllerBase
    {
        private readonly IActivitiesGetterService _activitiesGetterService;
        private readonly IToggleLikeService _toggleLikeService;
        private readonly IToggleSaveService _toggleSaveService;
        private readonly ICommentsAdderService _commentsAdderService;
        private readonly ICommentsGetterService _commentsGetterService;
        private readonly ICommentsRemoverService _commentsRemoverService;
        private readonly IQAAnswersCreatorService _QAAnswersCreatorService;
        private readonly IQAAnswersGetterService _QAAnswersGetterService;

        public ActivityController(IActivitiesGetterService activitiesGetterService, IToggleLikeService toggleLikeService, IToggleSaveService toggleSaveService, ICommentsAdderService commentsAdderService, ICommentsGetterService commentsGetterService, ICommentsRemoverService commentsRemoverService, IQAAnswersCreatorService qAAnswersCreatorService, IQAAnswersGetterService qAAnswersGetterService)
        {
            _activitiesGetterService = activitiesGetterService;
            _toggleLikeService = toggleLikeService;
            _toggleSaveService = toggleSaveService;
            _commentsAdderService = commentsAdderService;
            _commentsGetterService = commentsGetterService;
            _commentsRemoverService = commentsRemoverService;
            _QAAnswersCreatorService = qAAnswersCreatorService;
            _QAAnswersGetterService = qAAnswersGetterService;
        }

        [HttpGet("get")]
        public async Task<ActionResult<List<ActivityToResponse>>> GetActivity(string id)
        {
            ActivityToResponse activity = await _activitiesGetterService.GetActivity(id);

            return Ok(activity);
        }

        [HttpPost("toggle-like")]
        public async Task<ActionResult<LikesInformationToResponse>> ToggleLike([FromBody] ToggleLikeDTO dto)
        {
            LikesInformationToResponse likesInformationToResponse = await _toggleLikeService.ToggleLike(dto);

            return likesInformationToResponse;
        }

        [HttpPost("toggle-save")]
        public async Task<ActionResult<SaveInformationToResponse>> ToggleSave([FromBody] ToggleSaveDTO dto)
        {
            SaveInformationToResponse saveInformationToResponse = await _toggleSaveService.ToggleSave(dto);

            return saveInformationToResponse;
        }

        [HttpPost("add-comment")]
        public async Task<CommentToResponse> AddComment([FromBody] AddCommentDTO dto)
        {
            CommentToResponse commentToResponse = await _commentsAdderService.CreateComment(dto);

            return commentToResponse;
        }

        [HttpGet("get-comments")]
        public async Task<ActionResult<List<CommentToResponse>>> GetComments(string activityId, int page = 1, int pageSize = 3)
        {
            List<CommentToResponse> comments = await _commentsGetterService.GetCommentsPaged(activityId, page, pageSize);

            return Ok(comments);
        }

        [HttpPost("delete-comment")]
        public async Task<ActionResult<List<CommentToResponse>>> DeleteComment([FromBody] DeleteCommentDTO dto)
        {
            await _commentsRemoverService.DeleteComment(dto.CommentId, dto.UserId);

            return Ok();
        }

        [HttpPost("leave-qa-answer")]
        public async Task<IActionResult> LeaveQAAnswer(LeaveQAAnswerDTO dto)
        {
            await _QAAnswersCreatorService.LeaveQAAnswer(dto);

            return Ok();
        }

        [HttpGet("get-qa-answers")]
        public async Task<ActionResult<QAAnswerToResponse>> GetQAAnswers([FromQuery] string QAId)
        {
            var QAAnswers = await _QAAnswersGetterService.GetQAAnswers(QAId);

            return Ok(QAAnswers);
        }
    }
}