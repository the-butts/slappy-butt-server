﻿namespace SlappyButt.Api.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using SlappyButt.Api.DataTransferModels.Users;
    using SlappyButt.Api.Validation;
    using SlappyButt.Services.Data.Contracts;

    [RoutePrefix("api/users")]
    [EnableCors("*", "*", "*")]
    public class UsersController : ApiController
    {
        private readonly IUsersServices usersServices;

        public UsersController(IUsersServices usersServices)
        {
            this.usersServices = usersServices;
        }

        [HttpGet]
        [Authorize]
        [Route("avatar")]
        public async Task<IHttpActionResult> Get(string name)
        {
            var resultUser = await this.usersServices
                .GetByUserName(name)
                .ProjectTo<UserResponseModel>()
                .FirstOrDefaultAsync();

            if (resultUser == null)
            {
                return this.NotFound();
            }

            return this.Ok(resultUser);
        }

        [HttpPut]
        [Authorize]
        [ValidateModel]
        public async Task<IHttpActionResult> Put(UpdateUserRequestModel model)
        {
            var authenticatedUser = this.User.Identity.GetUserId();

            var updateUserResult = await this.usersServices.Update(
                authenticatedUser,
                model.Email);

            return this.Ok(updateUserResult);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IHttpActionResult> Delete()
        {
            var authenticatedUser = this.User.Identity.GetUserId();

            var deletedAlbumId = await this.usersServices.Delete(authenticatedUser);

            return this.Ok(deletedAlbumId);
        }
    }
}