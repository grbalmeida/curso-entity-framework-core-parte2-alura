using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using System;

namespace Alura.Filmes.App.Dados
{
    public class AluraFilmesContexto : DbContext
    {
        public DbSet<Ator> Atores { get; set; }
        public DbSet<Filme> Filmes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AluraFilmes;Trusted_Connection=true;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ator>()
                .ToTable("actor");

            modelBuilder.Entity<Ator>()
                .Property(a => a.Id)
                .HasColumnName("actor_id");

            modelBuilder.Entity<Ator>()
                .Property(a => a.PrimeiroNome)
                .HasColumnName("first_name")
                .HasColumnType("varchar(45)")
                .IsRequired();

            modelBuilder.Entity<Ator>()
                .Property(a => a.UltimoNome)
                .HasColumnName("last_name")
                .HasColumnType("varchar(45)")
                .IsRequired();

            modelBuilder.Entity<Ator>()
                .Property<DateTime>("last_update")
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Filme>()
                .ToTable("film");

            modelBuilder.Entity<Filme>()
                .Property(f => f.Id)
                .HasColumnName("film_id");

            modelBuilder.Entity<Filme>()
                .Property(f => f.Tituto)
                .HasColumnName("title")
                .HasColumnType("varchar(255)")
                .IsRequired();

            modelBuilder.Entity<Filme>()
                .Property(f => f.Descricao)
                .HasColumnName("description")
                .HasColumnType("text");

            modelBuilder.Entity<Filme>()
                .Property(f => f.AnoLancamento)
                .HasColumnName("release_year")
                .HasColumnType("varchar(4)");

            modelBuilder.Entity<Filme>()
                .Property(f => f.Duracao)
                .HasColumnName("length");

            modelBuilder.Entity<Filme>()
                .Property<DateTime>("last_update")
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");

            base.OnModelCreating(modelBuilder);
        }
    }
}
