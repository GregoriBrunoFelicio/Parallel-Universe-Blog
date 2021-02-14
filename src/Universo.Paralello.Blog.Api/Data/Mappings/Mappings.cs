using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Universo.Paralello.Blog.Api.Entities;

namespace Universo.Paralello.Blog.Api.Data.Mappings
{
    public class UsuarioMapping: IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).HasColumnType("varchar").IsRequired().HasMaxLength(50);
            builder.Property(x => x.Sobre).HasColumnType("varchar").HasMaxLength(500);
            builder.HasOne(x => x.Conta).WithOne(x => x.Usuario).HasForeignKey<Conta>(x => x.UsuarioId).IsRequired();
        }
    }

    public class ContaMapping: IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).HasColumnType("varchar").IsRequired().HasMaxLength(30);
            builder.OwnsOne(x => x.Senha).Property(x => x.Valor).HasColumnName("Senha").HasColumnType("varchar(MAX)").IsRequired();
        }
    }
}
