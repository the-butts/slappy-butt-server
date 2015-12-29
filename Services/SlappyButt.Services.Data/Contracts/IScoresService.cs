namespace SlappyButt.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;
    using SlappyButt.Common.Constants;
    using SlappyButt.Models;

    public interface IScoresService
    {
        IQueryable<Score> All(int take = GlobalConstants.DefaultTakeCount);

        IQueryable<Score> GetScoresByUserId(string userId, int take = GlobalConstants.DefaultTakeCount);

        Task<int> SubmitNewScoreToUserId(string userId, int scoreValue);
    }
}