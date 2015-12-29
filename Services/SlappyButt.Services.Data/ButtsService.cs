namespace SlappyButt.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using SlappyButt.Common.Constants;
    using SlappyButt.Data.Contracts;
    using SlappyButt.Models;
    using SlappyButt.Services.Data.Contracts;

    public class ButtsService : IButtsService
    {
        private readonly IRepository<Butt> butts;
        private readonly IRepository<User> users;

        public ButtsService(IRepository<Butt> buttsRepo, IRepository<User> usersRepo)
        {
            this.butts = buttsRepo;
            this.users = usersRepo;
        }

        public IQueryable<Butt> All()
        {
            var allButts = this.butts
                .All()
                .Where(s => (s.IsDeleted == false))
                .OrderByDescending(s => s.CreatedOn);

            return allButts;
        }

        public IQueryable<Butt> GetUserButtsByUserId(string userId)
        {
            var allScores = this.users
                .All()
                .Where(u => u.IsDeleted == false)
                .SelectMany(u => u.Butts);

            return allScores;
        }

        public async Task<int> AddNewButtToUserId(string userId, int buttId)
        {
            var currentUser = this.users
                 .All()
                 .FirstOrDefault(u => u.Id == userId && u.IsDeleted == false);

            var butt = this.butts
                .All()
                .FirstOrDefault(b => b.Id == buttId && b.IsDeleted == false);

            if (currentUser == null || butt == null)
            {
                return GlobalConstants.ItemNotFoundReturnValue;
            }

            currentUser.Butts.Add(butt);
            
            var result = GlobalConstants.ItemNotFoundReturnValue;

            try
            {
                result = await this.users.SaveChangesAsync();
            }
            catch (Exception e)
            {
                var er = e.Message;
            }

            return result;
        }
    }
}
