using AutoBogus;
using Parallel.Universe.Blog.Api.ViewModels;

namespace Parallel.Universe.Blog.Tests.Shared.Builders.ViewModels
{
    public sealed class AccountInputModelBuilder : AutoFaker<AccountInputModel>
    {
        public AccountInputModelBuilder()
        {
            RuleFor(x => x.Id, () => 0);
        }
    }
}
