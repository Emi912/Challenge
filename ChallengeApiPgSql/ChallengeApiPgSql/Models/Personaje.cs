namespace ChallengeApiPgSql.Models;

//Imagen.
//○ Nombre.
//○ Edad.
//○ Peso.
//○ Historia.
//○ Películas o series asociadas.

public class Personaje
{

    public Personaje()
    {
        Peliculas = new List<Pelicula>();
    }
    public int Id { get; set; }
    public string Imagen { get; set; } = String.Empty;
    public string Nombre { get; set; } = String.Empty;
    public int Edad { get; set; }
    public string Historia { get; set; } = String.Empty;
    public List<Pelicula> Peliculas { get; set; }
}
