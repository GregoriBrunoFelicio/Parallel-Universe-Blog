using Bogus;
using Universo.Paralello.Blog.Api.ViewModels;

namespace Universo.Paralello.Blog.Tests.Shared.Builders
{
    public sealed class ContaViewModelBuilder: Faker<ContaViewModel>
    {
        public ContaViewModelBuilder() =>
            RuleFor(f => f.Id, () => 0)
                .RuleFor(f => f.Email, f => f.Internet.Email())
                .RuleFor(f => f.Senha, f => f.Internet.Password())
                .RuleFor(f => f.SenhaConfirmacao, f => f.Internet.Password());
    }
}
