using Bogus;
using Universo.Paralello.Blog.Api.Entities;

namespace Universo.Paralello.Blog.Tests.Shared.Builders
{
    public sealed class UsuarioBuilder : Faker<Usuario>
    {
        public UsuarioBuilder() =>
            CustomInstantiator(f =>
                new Usuario(
                    f.Random.Int(1, 100),
                    f.Random.Word(),
                    new ContaBuilder()));
    }
}
