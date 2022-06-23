namespace ChallengeApiPgSql.Dto;

public class PeliculaDto
{
    public int Id { get; set; }
    public string Imagen { get; set; } = String.Empty;
    public string Titulo { get; set; } = String.Empty;
    public DateTime Estreno { get; set; }
    public int Calificacion { get; set; }
}
