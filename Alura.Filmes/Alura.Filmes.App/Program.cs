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
            UsandoViewsComEntity();
        }

        private static void UsandoViewsComEntity()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var sql = @"select a.*
                            from actor a
                            inner join top5_most_starred_actors filmes
                            on filmes.actor_id = a.actor_id";

                var atoresMaisAtuantes = contexto.Atores
                    .FromSql(sql)
                    .Include(a => a.Filmografia);

                foreach (var ator in atoresMaisAtuantes)
                {
                    Console.WriteLine($"O ator {ator.PrimeiroNome} {ator.UltimoNome} atuou em {ator.Filmografia.Count} filmes.");
                }
            }

            Console.ReadLine();
        }

        private static void FromSqlESuasLimitacoes()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var sql = @"select a.*
                            from actor a
                                inner join
                            (select top 5 a.actor_id, count(*) as total
                            from actor a
                                inner join film_actor fa on fa.actor_id = a.actor_id
                            group by a.actor_id
                            order by total desc) filmes on filmes.actor_id = a.actor_id";

                var atoresMaisAtuantes = contexto.Atores
                    .FromSql(sql)
                    .Include(a => a.Filmografia);

                foreach (var ator in atoresMaisAtuantes)
                {
                    Console.WriteLine($"O ator {ator.PrimeiroNome} {ator.UltimoNome} atuou em {ator.Filmografia.Count} filmes.");
                }
            }

            Console.ReadLine();
        }

        private static void AssumindoAsRedeasDoSQLGerado()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                //var atoresMaisAtuantes = contexto.Atores
                //    .Include(a => a.Filmografia)
                //    .OrderByDescending(a => a.Filmografia.Count)
                //    .Take(5);

                var sql = @"select top 5 a.first_name, a.last_name, count(*) as total
                            from actor a
                                inner join film_actor fa on fa.actor_id = a.actor_id
                            group by a.first_name, a.last_name
                            order by total desc";

                var atoresMaisAtuantes = contexto.Atores.FromSql(sql);

                foreach (var ator in atoresMaisAtuantes)
                {
                    Console.WriteLine($"O ator {ator.PrimeiroNome} {ator.UltimoNome} atuou em {ator.Filmografia.Count} filmes.");
                }
            }

            Console.ReadLine();
        }

        private static void MapeandoHeranca()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                foreach (var cliente in contexto.Clientes)
                {
                    Console.WriteLine(cliente);
                }

                foreach (var funcionario in contexto.Funcionarios)
                {
                    Console.WriteLine(funcionario);
                }

                Console.ReadLine();
            }
        }

        private static void ModeloDeDadosDoEntity()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var filme = new Filme
                {
                    Titulo = "Casino Royale",
                    Duracao = 120,
                    AnoLancamento = "2000",
                    Classificacao = ClassificacaoIndicativa.MaioresQue14,
                    IdiomaFalado = contexto.Idiomas.First()
                };

                contexto.Entry(filme).Property("last_update").CurrentValue = DateTime.Now;

                contexto.Filmes.Add(filme);

                contexto.SaveChanges();

                var filmeInserido = contexto.Filmes.First(f => f.Titulo == "Casino Royale");

                Console.WriteLine(filmeInserido.Classificacao);
            }

            Console.ReadLine();
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
                    Classificacao = ClassificacaoIndicativa.Livre,
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