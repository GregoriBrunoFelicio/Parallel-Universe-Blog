using Bogus;
using Universo.Paralello.Blog.Api.Entities;
using Universo.Paralello.Blog.Api.Shared.ValueObjects;

namespace Universo.Paralello.Blog.Tests.Shared.Builders
{
    public sealed class ContaBuilder: Faker<Conta>
    {
        public ContaBuilder() =>
            CustomInstantiator(f =>
                new Conta(f.Random.Int(1, 100),
                    f.Random.Word(), new Senha(f.Random.Words())));
    }
}
