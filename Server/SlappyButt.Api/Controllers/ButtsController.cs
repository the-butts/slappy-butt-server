namespace SlappyButt.Api.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using SlappyButt.Api.DataTransferModels.Avatars;
    using SlappyButt.Api.DataTransferModels.Butts;
    using SlappyButt.Api.Validation;
    using SlappyButt.Services.Data.Contracts;

    [RoutePrefix("api/butts")]
    [EnableCors("*", "*", "*")]
    public class ButtsController : ApiController
    {
        private readonly IButtsService buttsService;

        public ButtsController(IButtsService butts)
        {
            this.buttsService = butts;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var result = await this.buttsService
                .All()
                .ProjectTo<AvatarResponseModel>()
                .ToListAsync();

            return this.Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IHttpActionResult> GetByUser()
        {
            var currentUserId = this.User.Identity.GetUserId();

            var result = await this.buttsService
                .GetUserButtsByUserId(currentUserId)
                .ProjectTo<ButtResponseModel>()
                .ToListAsync();

            return this.Ok(result);
        }

        [HttpPost]
        [Authorize]
        [ValidateModel]
        public async Task<IHttpActionResult> Post(ButtRequestModel model)
        {
            var currentUserId = this.User.Identity.GetUserId();

            var changes = await this.buttsService
                .AddNewButtToUserId(currentUserId, model.Id);

            return this.Ok(changes);
        }
    }
}