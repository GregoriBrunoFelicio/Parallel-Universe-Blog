using Bogus;
using FluentAssertions;
using NUnit.Framework;
using Universo.Paralello.Blog.Api.Shared.ValueObjects;

namespace Universo.Paralello.Blog.Tests.UnitTests.ValueObjects
{
    public class SenhaTests
    {
        private readonly Faker _faker = new Faker();

        [Test]
        public void DeveCriptografarASenhaInformada()
        {
            var valor = _faker.Random.Word();
            var senha = new Senha(valor);
            senha.Criptografar();
            valor.Should().NotBe(senha.Valor);
        }

        [Test]
        public void DeveRetornarVerdadeiroSeASenhaEstiverCorreta()
        {
            var valor = _faker.Random.Word();
            var senha = new Senha(valor);
            senha.Criptografar();
            senha.Verificar(valor).Should().BeFalse();
        }

        [Test]
        public void DeveRetornarFalsoSeASenhaEstiverIncorreta()
        {
            var valor = _faker.Random.Word();
            var senha = new Senha(valor);
            senha.Criptografar();
            senha.Verificar($"{valor}123").Should().BeFalse();
        }
    }
}
