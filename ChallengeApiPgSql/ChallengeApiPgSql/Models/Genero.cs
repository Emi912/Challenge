namespace ChallengeApiPgSql.Models;

public class Genero
{
    public Genero()
    {
        Peliculas = new List<Pelicula>();
    }
    public int Id { get; set; }
    public string Nombre { get; set; } = String.Empty;
    public string Imagen { get; set; } = String.Empty;
    public List<Pelicula> Peliculas{ get; set; }
}
