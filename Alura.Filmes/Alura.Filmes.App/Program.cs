using Alura.Filmes.App.Dados;
using Alura.Filmes.App.Extensions;
using Alura.Filmes.App.Negocio;
using System;

namespace Alura.Filmes.App
{
    class Program
    {
        static void Main(string[] args)
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