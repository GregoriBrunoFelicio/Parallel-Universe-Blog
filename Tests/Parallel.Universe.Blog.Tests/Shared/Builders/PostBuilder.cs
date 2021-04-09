using Bogus;
using Parallel.Universe.Blog.Api.Entities;

namespace Parallel.Universe.Blog.Tests.Shared.Builders
{
    public sealed class PostBuilder : Faker<Post>
    {
        public PostBuilder() =>
            CustomInstantiator(f => new Post(
                0,
                f.Random.Words(),
                f.Random.Words(),
                f.Random.Words(),
                f.Date.Future(),
                f.Random.Bool(),
                0
            ));

        public PostBuilder WithActive(bool active)
        {
            RuleFor(x => x.Active, () => active);
            return this;
        }

        public PostBuilder WithUser(User user)
        {
            RuleFor(x => x.User, () => user);
            return this;
        }
    }
}
