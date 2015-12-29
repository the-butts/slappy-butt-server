namespace SlappyButt.Api.DataTransferModels.Butts
{
    using SlappyButt.Api.Infrastructure.Mapping;
    using SlappyButt.Models;

    public class ButtResponseModel : IMapFrom<Butt>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}