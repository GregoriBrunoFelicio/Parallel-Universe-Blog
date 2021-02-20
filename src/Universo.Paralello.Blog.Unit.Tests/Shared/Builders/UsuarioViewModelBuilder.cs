using Bogus;
using Universo.Paralello.Blog.Api.ViewModels;

namespace Universo.Paralello.Blog.Tests.Shared.Builders
{
    public sealed class UsuarioViewModelBuilder : Faker<UsuarioViewModel>
    {
        public UsuarioViewModelBuilder() =>
            RuleFor(f => f.Id, () => 0)
                .RuleFor(f => f.Nome, f => f.Random.Words())
                .RuleFor(f => f.Sobre, f => f.Random.Words());

        public UsuarioViewModelBuilder WithConta()
        {
            RuleFor(f => f.Conta, f => new ContaViewModelBuilder());
            return this;
        }
    }
}
