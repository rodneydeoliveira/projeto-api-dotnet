using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using WebPessoa.Repository.Models;

// Criamos a camada de Repository para conectar ao banco de dados
// A pasta Models, faz referência a TABELA, no banco de dados
// O arquivo Context, faz referência ao nosso banco de dados

namespace WebPessoa.Repository
{
    public class PessoaContext : DbContext

    {
        public PessoaContext(DbContextOptions<PessoaContext> options) : base(options) { }

        public DbSet<TabUsuario> Usuarios { get; set; }
        public DbSet<TabPessoa> Pessoas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TabUsuario>().ToTable("tabUsuario");
            modelBuilder.Entity<TabPessoa>().ToTable("tabPessoa");
        }
    }
}
