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
            ColunaClassificacaoIndicativa();   
        }

        private static void ColunaClassificacaoIndicativa()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var idioma = contexto.Idiomas.First();

                var filme = new Filme
                {
                    Titulo = "Senhor dos Anéis",
                    Duracao = 120,
                    AnoLancamento = "2000",
                    Classificacao = "Qualquer",
                    IdiomaFalado = idioma
                };

                contexto.Filmes.Add(filme);
                contexto.SaveChanges();
            }

            Console.ReadLine();
        }

        private static void ConfigurandoRestricoesUnique()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var ator1 = new Ator
                {
                    PrimeiroNome = "Emma",
                    UltimoNome = "Watson"
                };

                var ator2 = new Ator
                {
                    PrimeiroNome = "Emma",
                    UltimoNome = "Watson"
                };

                contexto.Atores.AddRange(ator1, ator2);
                contexto.SaveChanges();

                var emmaWatson = contexto.Atores
                    .Where(a => a.PrimeiroNome == "Emma" && a.UltimoNome == "Watson");

                Console.WriteLine($"Total de atores encontrados: {emmaWatson.Count()}");
            }

            Console.ReadLine();
        }

        private static void IdiomaFaladoEOriginal()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var filme = contexto.Filmes
                    .Include(f => f.IdiomaFalado)
                    .Include(f => f.IdiomaOriginal)
                    .First();

                Console.WriteLine(filme);
                Console.WriteLine($"Idioma falado: {filme.IdiomaFalado.Nome}");
                Console.WriteLine($"Idioma original: {filme.IdiomaOriginal.Nome}");
            }

            Console.ReadLine();
        }

        private static void MultiplosRelacionamentosMesmasTabelas()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                foreach (var idioma in contexto.Idiomas)
                {
                    Console.WriteLine(idioma);
                }
            }

            Console.ReadLine();
        }

        private static void DesafioMapeamentoFilmesCategorias()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var categoria = contexto.Categorias
                    .Include(c => c.Filmes)
                    .ThenInclude(cf => cf.Filme)
                    .First();

                Console.WriteLine(categoria);
                Console.WriteLine(contexto.Entry(categoria).Property<DateTime>("last_update").CurrentValue);
                Console.WriteLine("Filmes:");

                foreach (var item in categoria.Filmes)
                {
                    Console.WriteLine(item.Filme);
                }
            }

            Console.ReadLine();
        }

        private static void ConfigurandoChavesEstrangeiras()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var filme = contexto.Filmes
                    .Include(f => f.Atores)
                    .ThenInclude(fa => fa.Ator)
                    .First();

                Console.WriteLine(filme);
                Console.WriteLine("Elenco:");

                foreach (var item in filme.Atores)
                {
                    Console.WriteLine(item.Ator);
                }
            }

            Console.ReadLine();
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