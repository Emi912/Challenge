using ChallengeApiPgSql.Models;
using Microsoft.EntityFrameworkCore;

namespace ChallengeApiPgSql.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {

    }


public DbSet<Personaje> Personajes { get; set; }
    public DbSet<Genero> Generos { get; set; }
    public DbSet<Pelicula> Peliculas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
}
