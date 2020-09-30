namespace Alura.Filmes.App.Negocio
{
    public class Idioma
    {
        public byte Id { get; set; }
        public string Nome { get; set; }

        public override string ToString()
        {
            return $"Idioma ({Id}): {Nome}";
        }
    }
}
