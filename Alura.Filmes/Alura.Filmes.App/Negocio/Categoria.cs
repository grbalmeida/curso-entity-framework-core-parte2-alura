using System.Collections.Generic;

namespace Alura.Filmes.App.Negocio
{
    public class Categoria
    {
        public byte Id { get; set; }
        public string Nome { get; set; }
        public IList<FilmeCategoria> Filmes { get; set; }

        public override string ToString()
        {
            return $"Categoria ({Id}): {Nome}";
        }
    }
}
