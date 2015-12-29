namespace SlappyButt.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;
    using SlappyButt.Models;

    public interface IButtsService
    {
        IQueryable<Butt> All();

        IQueryable<Butt> GetUserButtsByUserId(string userId);

        Task<int> AddNewButtToUserId(string userId, int buttId);
    }
}