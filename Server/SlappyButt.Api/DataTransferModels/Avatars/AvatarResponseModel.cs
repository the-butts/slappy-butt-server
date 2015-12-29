namespace SlappyButt.Api.DataTransferModels.Avatars
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using SlappyButt.Api.Infrastructure.Mapping;
    using SlappyButt.Models;

    public class AvatarResponseModel : IMapFrom<Avatar>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public byte[] ByteArrayContent { get; set; }

        public string FileExstension { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<Avatar, AvatarResponseModel>()
                .ForMember(a => a.UserName, opts => opts.MapFrom(a => a.User.UserName))
                .ForMember(a => a.ByteArrayContent, opts => opts.MapFrom(a => a.ImageInfo.ByteArrayContent))
                .ForMember(a => a.FileExstension, opts => opts.MapFrom(a => a.ImageInfo.OriginalExtension));
        }
    }
}