using System.Text.Json.Serialization;

namespace ChallengeApiPgSql.Models;

//Película o Serie: deberá tener,
//○ Imagen.

//○ Título.
//○ Fecha de creación.
//○ Calificación(del 1 al 5).
//○ Personajes asociados.
public class Pelicula
{
    public Pelicula()
    {
        Personajes = new List<Personaje>();
    }
    public int Id { get; set; }
    public string Imagen { get; set; } = String.Empty;
    public string Titulo { get; set; } = String.Empty;
    public DateTime Estreno { get; set; }
    public int Calificacion { get; set; }
    [JsonIgnore]
    public List<Personaje> Personajes { get; set; }
}
