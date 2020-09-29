using Alura.Filmes.App.Dados;
using Alura.Filmes.App.Extensions;
using Alura.Filmes.App.Negocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Alura.Filmes.App
{
    class Program
    {
        static void Main(string[] args)
        {
            ShadowPropertiesLINQWhere();
        }

        private static void ShadowPropertiesLINQWhere()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                // Listar os 10 atores modificados recentemente em 2017

                var atores = contexto.Atores
                    .Where(a => EF.Property<DateTime>(a, "last_update").Year == 2017)
                    .OrderByDescending(a => EF.Property<DateTime>(a, "last_update"))
                    .Take(10);

                foreach (var ator in atores)
                {
                    Console.WriteLine(ator);
                    Console.WriteLine(contexto.Entry(ator).Property("last_update").CurrentValue);
                }
            }

            Console.ReadLine();
        }

        private static void ShadowPropertiesLINQOrderByDescending()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                // Listar os 10 atores modificados recentemente
                var atores = contexto.Atores
                    .OrderByDescending(a => EF.Property<DateTime>(a, "last_update"))
                    .Take(10);

                foreach (var ator in atores)
                {
                    Console.WriteLine(ator);
                    Console.WriteLine(contexto.Entry(ator).Property("last_update").CurrentValue);
                }
            }

            Console.ReadLine();
        }

        private static void RecuperandoShadowProperties()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var ator = contexto.Atores.First();

                Console.WriteLine(ator);
                Console.WriteLine(contexto.Entry(ator).Property("last_update").CurrentValue);
            }

            Console.ReadLine();
        }

        private static void DefinindoShadowProperties()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var ator = new Ator
                {
                    PrimeiroNome = "Tom",
                    UltimoNome = "Hanks"
                };

                contexto.Entry(ator).Property<DateTime>("last_update").CurrentValue = DateTime.Now;

                contexto.Atores.Add(ator);

                contexto.SaveChanges();
            }

            Console.ReadLine();
        }
    }
}