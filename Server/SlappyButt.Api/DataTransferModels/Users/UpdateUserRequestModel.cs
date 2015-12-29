namespace SlappyButt.Api.DataTransferModels.Users
{
    using SlappyButt.Api.DataTransferModels.Avatars;

    public class UpdateUserRequestModel
    {
        public string Email { get; set; }

        public AvaratRequstModel Avatar { get; set; }
    }
}