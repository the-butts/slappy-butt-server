namespace SlappyButt.Api.DataTransferModels.Scores
{
    using System;
    using AutoMapper;
    using SlappyButt.Api.Infrastructure.Mapping;
    using SlappyButt.Models;

    public class ScoreResponseModel : IMapFrom<Score>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public DateTime SubmitedOn { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<Score, ScoreResponseModel>()
            .ForMember(u => u.UserName, opts => opts.MapFrom(u => u.User.UserName));
        }
    }
}