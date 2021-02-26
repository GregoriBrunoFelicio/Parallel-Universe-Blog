using Bogus;
using Parallel.Universe.Blog.Api.Entities;

namespace Parallel.Universe.Blog.Tests.Shared.Builders
{
    public sealed class UsuarioBuilder : Faker<User>
    {
        public UsuarioBuilder() =>
            CustomInstantiator(f =>
                new User(
                    f.Random.Int(1, 100),
                    f.Random.Word(),
                    new AccountBuilder()));
    }
}
