using Bogus;
using Parallel.Universe.Blog.Api.Entities;

namespace Parallel.Universe.Blog.Tests.Shared.Builders.Models
{
    public sealed class UserBuilder : Faker<User>
    {
        public UserBuilder() =>
            CustomInstantiator(f =>
                new User(
                    0,
                    f.Random.Word(),
                    f.Random.Words(),
                    new AccountBuilder(),
                    f.Random.Bool()));

        public UserBuilder WithActive(bool active)
        {
            CustomInstantiator(f => new User(0, f.Random.Word(), f.Random.Words(), new AccountBuilder(), active));
            return this;
        }

        public UserBuilder WithId(int id)
        {
            CustomInstantiator(f => new User(id, f.Random.Word(), f.Random.Words(), new AccountBuilder(), f.Random.Bool()));
            return this;
        }
    }
}
