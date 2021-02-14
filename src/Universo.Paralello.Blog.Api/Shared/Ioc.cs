using Microsoft.Extensions.DependencyInjection;
using Universo.Paralello.Blog.Api.Data.Repositories;
using Universo.Paralello.Blog.Api.Services;

namespace Universo.Paralello.Blog.Api.Shared
{
    public static class Ioc
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IContaRepository, ContaRepository>();
            services.AddScoped<IContaService, ContaService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
