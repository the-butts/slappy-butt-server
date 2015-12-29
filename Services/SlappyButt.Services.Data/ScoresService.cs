namespace SlappyButt.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using SlappyButt.Common.Constants;
    using SlappyButt.Data.Contracts;
    using SlappyButt.Models;
    using SlappyButt.Services.Data.Contracts;

    public class ScoresService : IScoresService
    {
        private readonly IRepository<Score> scores;
        private readonly IRepository<User> users;

        public ScoresService(IRepository<Score> scoresRepo, IRepository<User> usersRepo)
        {
            this.scores = scoresRepo;
            this.users = usersRepo;
        }

        public IQueryable<Score> All(int take = GlobalConstants.DefaultTakeCount)
        {
            var allScores = this.scores
                .All()
                .Where(s => (s.IsDeleted == false))
                .OrderByDescending(s => s.Value)
                .Take(take);

            return allScores;
        }

        public IQueryable<Score> GetScoresByUserId(string userId, int take = GlobalConstants.DefaultTakeCount)
        {
            var allScores = this.scores
                .All()
                .Where(s => (s.IsDeleted == false) 
                    && s.UserId == userId)
                .OrderByDescending(s => s.SubmitedOn)
                .Take(take);

            return allScores;
        }

        public async Task<int> SubmitNewScoreToUserId(string userId, int scoreValue)
        {
            var currentUser = this.users
                 .All()
                 .FirstOrDefault(u => u.Id == userId 
                    && u.IsDeleted == false);

            if (currentUser == null)
            {
                return GlobalConstants.ItemNotFoundReturnValue;
            }

            var newScore = new Score()
            {
                Value = scoreValue,
                User = currentUser,
                SubmitedOn = DateTime.Now
            };

            try
            {
                this.scores.Add(newScore);
                await this.scores.SaveChangesAsync();
            }
            catch (Exception e)
            {
                var er = e.Message;
            }

            return newScore.Id;
        }
    }
}
