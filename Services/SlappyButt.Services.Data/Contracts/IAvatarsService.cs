namespace SlappyButt.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;
    using SlappyButt.Common.Models;
    using SlappyButt.Models;

    public interface IAvatarsService
    {
        IQueryable<Avatar> All(int take);

        IQueryable<Avatar> GetAvatarByUserId(string userId);

        Task<int> AddVatarToUser(string userId, RawImage rawImage);

        Task<RawImage> ProcessImage(RawImage rawImage);
    }
}