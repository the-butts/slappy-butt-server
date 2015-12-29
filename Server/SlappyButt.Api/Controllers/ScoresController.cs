namespace SlappyButt.Api.Controllers
{
    using System.Data.Entity;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using SlappyButt.Api.DataTransferModels.Avatars;
    using SlappyButt.Api.DataTransferModels.Scores;
    using SlappyButt.Api.Validation;
    using SlappyButt.Common.Constants;
    using SlappyButt.Common.Extensions;
    using SlappyButt.Services.Data.Contracts;

    [RoutePrefix("api/scores")]
    [EnableCors("*", "*", "*")]
    public class ScoresController : ApiController
    {
        public const string EntityName = "Score";
        
        private readonly IScoresService scoresService;

        public ScoresController(
            IScoresService scoresService)
        {
            this.scoresService = scoresService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int take = GlobalConstants.DefaultTakeCount)
        {
            var result = await this.scoresService
                .All(take)
                .ProjectTo<ScoreResponseModel>()
                .ToListAsync();

            return this.Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var currentUserId = this.User.Identity.GetUserId();

            var result = await this.scoresService
                .GetScoresByUserId(currentUserId)
                .ProjectTo<ScoreResponseModel>()
                .ToListAsync();

            return this.Ok(result);
        }

        [HttpPost]
        [Authorize]
        [ValidateModel]
        public async Task<IHttpActionResult> Post(ScoreRequestModel model)
        {
            var currentUserId = this.User.Identity.GetUserId();

            var scoreId = await this.scoresService.SubmitNewScoreToUserId(currentUserId, model.Value);

            return this.Ok(scoreId);
        }
    }
}