using Bogus;
using Parallel.Universe.Blog.Api.ViewModels;

namespace Parallel.Universe.Blog.Tests.Shared.Builders
{
    public sealed class PostViewModelBuilder : Faker<PostViewModel>
    {
        public PostViewModelBuilder() =>
                  RuleFor(x => x.Id, () => 0)
                      .RuleFor(x => x.Title, f => f.Random.Words())
                      .RuleFor(x => x.Description, f => f.Random.Words())
                      .RuleFor(x => x.Text, f => f.Random.Words())
                      .RuleFor(x => x.Date, f => f.Date.Future());

        public PostViewModelBuilder WithActive(bool active)
        {
            RuleFor(x => x.Id, () => 0)
                       .RuleFor(x => x.Title, f => f.Random.Words())
                       .RuleFor(x => x.Description, f => f.Random.Words())
                       .RuleFor(x => x.Text, f => f.Random.Words())
                       .RuleFor(x => x.Date, f => f.Date.Future())
                       .RuleFor(x => x.Active, () => active);
            return this;
        }

        public PostViewModelBuilder WithUserId(int userId)
        {
            RuleFor(x => x.Id, () => 0)
                .RuleFor(x => x.Title, f => f.Random.Words())
                .RuleFor(x => x.Description, f => f.Random.Words())
                .RuleFor(x => x.Text, f => f.Random.Words())
                .RuleFor(x => x.UserId, () => userId)
                .RuleFor(x => x.Date, f => f.Date.Future());
            return this;
        }

    }
}
