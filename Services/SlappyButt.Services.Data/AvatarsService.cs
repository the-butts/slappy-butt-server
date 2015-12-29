namespace SlappyButt.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using SlappyButt.Common.Constants;
    using SlappyButt.Common.Models;
    using SlappyButt.Data.Contracts;
    using SlappyButt.Models;
    using SlappyButt.Services.Data.Contracts;
    using SlappyButt.Services.Logic.Contracts;

    public class AvatarsService : IAvatarsService
    {
        private readonly IRepository<Avatar> avatars;
        private readonly IRepository<User> users;
        private readonly IImageProcessorService imageProcessor;

        public AvatarsService(
            IRepository<Avatar> avatarsRepo,
            IRepository<User> usersRepo,
            IImageProcessorService imageProcessor)
        {
            this.avatars = avatarsRepo;
            this.users = usersRepo;
            this.imageProcessor = imageProcessor;
        }

        public IQueryable<Avatar> All(int take)
        {
            var allAvatars = this.avatars
                .All()
                .Where(s => (s.IsDeleted == false))
                .OrderByDescending(s => s.Id)
                .Take(take);

            return allAvatars;
        }

        public IQueryable<Avatar> GetAvatarByUserId(string userId)
        {
            var allScores = this.avatars
                .All()
                .Where(a => a.IsDeleted == false && a.UserId == userId);

            return allScores;
        }

        public async Task<int> AddVatarToUser(string userId, RawImage rawImage)
        {
            var currentUser = this.users
                 .All()
                 .FirstOrDefault(u => u.Id == userId
                    && u.IsDeleted == false);

            if (currentUser == null)
            {
                return GlobalConstants.ItemNotFoundReturnValue;
            }

            var processedImage = await this.ProcessImage(rawImage);

            var newImageInfo = new ImageInfo()
            {
                OriginalName = processedImage.OriginalFileName,
                OriginalExtension = processedImage.FileExtension,
                ByteArrayContent = processedImage.Content
            };

            var newAvatar = new Avatar()
            {
                ImageInfo = newImageInfo,
                User = currentUser
            };
            
            try
            {
                
                await this.avatars.SaveChangesAsync();
            }
            catch (Exception e)
            {
                var er = e.Message;
            }

            return newAvatar.Id;
        }

        public async Task<RawImage> ProcessImage(RawImage rawImage)
        {
            rawImage.Content = await this.imageProcessor
                .Resize(rawImage.Content, GlobalConstants.ResizedIAvatarWidth);

            return rawImage;
        }
    }
}
