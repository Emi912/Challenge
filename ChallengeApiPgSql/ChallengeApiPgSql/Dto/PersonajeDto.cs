namespace ChallengeApiPgSql.Dto;

public class PersonajeDto
{
    public int Id { get; set; }
    public string Imagen { get; set; } = String.Empty;
    public string Nombre { get; set; } = String.Empty;
    public int Edad { get; set; }
    public string Historia { get; set; } = String.Empty;
    
}
