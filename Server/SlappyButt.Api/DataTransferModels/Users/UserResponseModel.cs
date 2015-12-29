namespace SlappyButt.Api.DataTransferModels.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using SlappyButt.Api.DataTransferModels.Scores;
    using SlappyButt.Api.Infrastructure.Mapping;
    using SlappyButt.Models;

    public class UserResponseModel : IMapFrom<User>, IHaveCustomMappings
    {
        private ICollection<ScoreResponseModel> scores;

        public UserResponseModel()
        {
            this.scores = new HashSet<ScoreResponseModel>();
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public virtual ICollection<ScoreResponseModel> Scores
        {
            get { return this.scores; }
            set { this.scores = value; }
        }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<User, UserResponseModel>()
              .ForMember(urm => urm.Scores, opts => opts.MapFrom(u => u.Scores.OrderByDescending(s => s.Value)));
        }
    }
}