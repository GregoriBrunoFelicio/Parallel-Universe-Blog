using AutoMapper;
using Parallel.Universe.Blog.Api.Entities;
using Parallel.Universe.Blog.Api.Shared.ValueObjects;
using Parallel.Universe.Blog.Api.ViewModels;

namespace Parallel.Universe.Blog.Api.Shared.AutoMapper
{
    public class UserMappingProfile: Profile
    {
        public UserMappingProfile() =>
            CreateMap<UserViewModel, User>().ConstructUsing(x =>
                new User(x.Id, x.Name, new Account(x.Account.Id, x.Account.Email, new Password(x.Account.Email)), x.Active));
    }
}
