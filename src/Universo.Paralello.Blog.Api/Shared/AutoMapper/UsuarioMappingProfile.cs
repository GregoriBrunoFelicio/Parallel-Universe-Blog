using AutoMapper;
using Universo.Paralello.Blog.Api.Entities;
using Universo.Paralello.Blog.Api.Shared.ValueObjects;
using Universo.Paralello.Blog.Api.ViewModels;

namespace Universo.Paralello.Blog.Api.Shared.AutoMapper
{
    public class UsuarioMappingProfile: Profile
    {
        public UsuarioMappingProfile()
        {
            CreateMap<UsuarioViewModel, Usuario>().ConstructUsing(x =>
                new Usuario(x.Id, x.Nome, new Conta(x.Conta.Id, x.Conta.Email, new Senha(x.Conta.Senha))));
        }
    }
}
