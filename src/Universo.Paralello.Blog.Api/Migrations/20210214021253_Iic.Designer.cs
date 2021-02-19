﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Universo.Paralello.Blog.Api.Data;

namespace Universo.Paralello.Blog.Api.Migrations
{
    [DbContext(typeof(UniversoParalelloBlogContext))]
    [Migration("20210214021253_Iic")]
    partial class Iic
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Universo.Paralello.Blog.Api.Entities.Conta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("Conta");
                });

            modelBuilder.Entity("Universo.Paralello.Blog.Api.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Sobre")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Universo.Paralello.Blog.Api.Entities.Conta", b =>
                {
                    b.HasOne("Universo.Paralello.Blog.Api.Entities.Usuario", "Usuario")
                        .WithOne("Conta")
                        .HasForeignKey("Universo.Paralello.Blog.Api.Entities.Conta", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Universo.Paralello.Blog.Api.Shared.Senha", "Senha", b1 =>
                        {
                            b1.Property<int>("ContaId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Valor")
                                .IsRequired()
                                .HasColumnType("varchar(MAX)")
                                .HasColumnName("Senha");

                            b1.HasKey("ContaId");

                            b1.ToTable("Conta");

                            b1.WithOwner()
                                .HasForeignKey("ContaId");
                        });

                    b.Navigation("Senha");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Universo.Paralello.Blog.Api.Entities.Usuario", b =>
                {
                    b.Navigation("Conta");
                });
#pragma warning restore 612, 618
        }
    }
}