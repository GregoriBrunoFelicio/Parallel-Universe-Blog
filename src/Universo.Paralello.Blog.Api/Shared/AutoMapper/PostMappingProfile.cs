using AutoMapper;
using Parallel.Universe.Blog.Api.Entities;
using Parallel.Universe.Blog.Api.ViewModels;

namespace Parallel.Universe.Blog.Api.Shared.AutoMapper
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile() =>
            CreateMap<PostViewModel, Post>().ConstructUsing(x =>
                new Post(x.Id, x.Title, x.Description, x.Text, x.Date, x.Active, x.UserId));
    }
}
