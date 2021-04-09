using Bogus;
using Parallel.Universe.Blog.Api.ViewModels;

namespace Parallel.Universe.Blog.Tests.Shared.Builders
{
    public sealed class UserViewModelBuilder : Faker<UserViewModel>
    {
        public UserViewModelBuilder() =>
            RuleFor(f => f.Id, () => 0)
                .RuleFor(f => f.Name, f => f.Random.Words())
                .RuleFor(f => f.About, f => f.Random.Words());

        public UserViewModelBuilder WithAccount()
        {
            RuleFor(f => f.Account, f => new AccountViewModelBuilder());
            return this;
        }
    }
}
