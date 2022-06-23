namespace ChallengeApiPgSql.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Username { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;

}


