using AutoBogus;
using Parallel.Universe.Blog.Api.ViewModels;

namespace Parallel.Universe.Blog.Tests.Shared.Builders.ViewModels
{
    public sealed class PostViewModelBuilder : AutoFaker<PostViewModel>
    {
        public PostViewModelBuilder()
        {
            RuleFor(x => x.Id, () => 0);
        }

        public PostViewModelBuilder WithActive(bool active)
        {
            RuleFor(x => x.Active, () => active);
            return this;
        }

        public PostViewModelBuilder WithUserId(int userId)
        {
            RuleFor(x => x.UserId, () => userId);
            return this;
        }
    }
}
