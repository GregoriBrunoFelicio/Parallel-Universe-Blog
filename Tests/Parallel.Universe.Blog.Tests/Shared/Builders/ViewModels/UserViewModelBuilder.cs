using AutoBogus;
using Parallel.Universe.Blog.Api.ViewModels;

namespace Parallel.Universe.Blog.Tests.Shared.Builders.ViewModels
{
    public sealed class UserViewModelBuilder : AutoFaker<UserViewModel>
    {
        public UserViewModelBuilder()
        {
            RuleFor(x => x.Id, () => 0);
        }

        public UserViewModelBuilder WithAccount()
        {
            RuleFor(f => f.Account, () => new AccountInputModelBuilder());
            return this;
        }
    }
}
