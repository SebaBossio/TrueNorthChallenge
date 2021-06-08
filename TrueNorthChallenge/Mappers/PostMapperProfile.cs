using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueNorthChallenge.DBEntities;
using TrueNorthChallenge.Common.DTO;

namespace TrueNorthChallenge.Mappers
{
    public class PostMapperProfile : Profile
    {
        public PostMapperProfile()
        {
            CreateMap<PostDetailsModel, Post>()
                .ForMember(dest => dest.Posts_Tags, opt => opt.MapFrom((src, dest, arg, context) =>
                {
                    ICollection<Posts_Tags> posts_Tags = new List<Posts_Tags>();
                    foreach (var tagName in src.Tags)
                    {
                        posts_Tags.Add(new Posts_Tags()
                        {
                            CTS = DateTime.Now,
                            Tag = new Tag()
                            {
                                CTS = DateTime.Now,
                                Name = tagName
                            }
                        });
                    }

                    return posts_Tags;
                }));

            CreateMap<Post, PostDetailsModel>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src =>
                    src.Posts_Tags.Select(x => x.Tag.Name).ToList()
                ));

            CreateMap<Post, PostListItemModel>()
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src =>
                   string.IsNullOrEmpty(src.Content)
                   ? src.Content
                   : string.Format("{0}...", src.Content.Substring(0, System.Math.Min(src.Content.Length, 50))))); // getting 50 first symbols of a post from post content
        }
    }
}
