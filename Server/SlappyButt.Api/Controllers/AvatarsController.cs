namespace SlappyButt.Api.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using SlappyButt.Api.DataTransferModels.Avatars;
    using SlappyButt.Api.Validation;
    using SlappyButt.Common.Constants;
    using SlappyButt.Services.Data.Contracts;

    [RoutePrefix("api/avatars")]
    [EnableCors("*", "*", "*")]
    public class AvatarsController : ApiController
    {
        private readonly IAvatarsService avatarsService;

        public AvatarsController(IAvatarsService avatars)
        {
            this.avatarsService = avatars;
        }
        [HttpGet]
        public async Task<IHttpActionResult> Get(int take = GlobalConstants.DefaultTakeCount)
        {
            var result = await this.avatarsService
                .All(take)
                .ProjectTo<AvatarResponseModel>()
                .ToListAsync();

            return this.Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var currentUserId = this.User.Identity.GetUserId();

            var result = await this.avatarsService
                .GetAvatarByUserId(currentUserId)
                .ProjectTo<AvatarResponseModel>()
                .FirstOrDefaultAsync();

            return this.Ok(result);
        }

        [HttpPost]
        [Authorize]
        [ValidateModel]
        public async Task<IHttpActionResult> Post(AvaratRequstModel model)
        {
            var currentUserId = this.User.Identity.GetUserId();

            var scoreId = await this.avatarsService
                .AddVatarToUser(currentUserId, model.ToRawImage());

            return this.Ok(scoreId);
        }
    }
}