namespace SlappyButt.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;
    using SlappyButt.Models;

    public interface IUsersServices : IService
    {
        IQueryable<User> GetByUserName(string userId);

        Task<string> Delete(string userId);

        Task<string> Update(string userId, string newEmail = null);
    }
}