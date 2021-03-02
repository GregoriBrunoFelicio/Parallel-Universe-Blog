using Bogus;
using Parallel.Universe.Blog.Api.Entities;

namespace Parallel.Universe.Blog.Tests.Shared.Builders
{
    public sealed class UserBuilder : Faker<User>
    {
        public UserBuilder() =>
            CustomInstantiator(f =>
                new User(
                    f.Random.Int(1, 100),
                    f.Random.Word(),
                    new AccountBuilder(), f.Random.Bool()));

        public UserBuilder WithActive(bool active)
        {
            RuleFor(x => x.Active, active);
            return this;
        }
    }
}
