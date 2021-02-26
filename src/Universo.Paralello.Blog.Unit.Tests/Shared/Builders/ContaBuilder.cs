using Bogus;
using Parallel.Universe.Blog.Api.Entities;
using Parallel.Universe.Blog.Api.Shared.ValueObjects;

namespace Parallel.Universe.Blog.Tests.Shared.Builders
{
    public sealed class AccountBuilder: Faker<Account>
    {
        public AccountBuilder() =>
            CustomInstantiator(f =>
                new Account(f.Random.Int(1, 100),
                    f.Internet.Email(), new Password(f.Random.Words())));
    }
}
