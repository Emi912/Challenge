using ChallengeApiPgSql.Data;
using ChallengeApiPgSql.Dto;
using ChallengeApiPgSql.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChallengeApiPgSql.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PeliculasController : ControllerBase
{

    private readonly DataContext _context;

    public PeliculasController(DataContext context)
    {
        _context = context;
    }
    [HttpPost]
    public async Task<ActionResult<List<Pelicula>>> CrearPelicula(PeliculaDto pelicula)
    {
        var nPelicula = new Pelicula()
        {
            Id = pelicula.Id,
            Imagen = pelicula.Imagen,
            Titulo = pelicula.Titulo,
            Estreno = pelicula.Estreno,
            Calificacion = pelicula.Calificacion,
            Personajes = new List<Personaje>()
        };
        _context.Peliculas.Add(nPelicula);
        await _context.SaveChangesAsync();
        return Ok(await _context.Peliculas.ToListAsync());
    }

    [HttpGet("id")]
    public async Task<ActionResult<Pelicula>> GetById(int id)
    {
        var pelicula = await _context.Peliculas.FindAsync(id);
        if (pelicula == null)
        {
            return BadRequest("No existe pelicula con ese id");
        }
        else
        {
            return Ok(pelicula);
        }
    }

    [HttpPut]

    public async Task<ActionResult<Pelicula>> UpdatePelicula(PeliculaDto p)
    {
        var updPelicula = await _context.Peliculas.FindAsync(p.Id);
        if (updPelicula == null)
        {
            return BadRequest("No existe la pelicula que intenta editar");
        }
        else
        {
            updPelicula.Id = p.Id;
            updPelicula.Titulo = p.Titulo;
            updPelicula.Estreno = p.Estreno;
            updPelicula.Imagen = p.Imagen;
            updPelicula.Calificacion = p.Calificacion;
            await _context.SaveChangesAsync();
            return Ok(updPelicula);
        }
    }

    [HttpDelete("id")]
    public async Task<ActionResult<List<Pelicula>>> DeletePelicula(int id)
    {
        var dPelicula = await _context.Peliculas.FindAsync(id);
        if (dPelicula == null)
        {
            return BadRequest("No existe pelicula con ese id");
        }
        else
        {
            _context.Peliculas.Remove(dPelicula);
            await _context.SaveChangesAsync();
            return Ok(await _context.Peliculas.ToListAsync());
        }
    }

    [HttpPost("id")]
    public async Task<ActionResult<Pelicula>> AddPersonaje(PersonajeDto personajeDto, int id)
    {
        var pelicula = await _context.Peliculas.FindAsync(id);
        if (pelicula == null)
        {
            return BadRequest();
        }
        else
        {
            var personaje = _context.Personajes.FirstOrDefault(p=> p.Id == personajeDto.Id);
            if (personaje == null)
            {
                var nPersonaje = new Personaje
                {
                    Id = personajeDto.Id,
                    Imagen = personajeDto.Imagen,
                    Nombre = personajeDto.Nombre,
                    Historia = personajeDto.Historia,
                    Edad = personajeDto.Edad,
                    Peliculas = new List<Pelicula>()
                {
                    pelicula
                }
                };
                nPersonaje.Peliculas.Add(pelicula);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                personaje.Peliculas.Add(pelicula);
                await _context.SaveChangesAsync();
                return Ok();
            }
            
        }
    }
    [HttpGet]
    public async Task<ActionResult<List<Pelicula>>> GetAll()
    {
        return Ok(await _context.Peliculas.ToListAsync());
    }

    [HttpGet("nombre")]

    public async Task<ActionResult<List<Pelicula>>>GetByNombre(string nombre)
    {
        var pelicula = await _context.Peliculas.Where(p=> p.Titulo == nombre).ToListAsync();
        if (pelicula == null)
        {
            return BadRequest("No existe pelicula con ese nombre");
        }
        return Ok(pelicula);
    }

    [HttpGet("order")]
    public async Task<ActionResult<List<Pelicula>>> GetByOrder(string order)
    {
        List<Pelicula> peliculas = new List<Pelicula>();
        switch (order)
        {
            case "ASC":
                peliculas = _context.Peliculas.OrderBy(p => p.Id).ToList();
                break;
            case "DESC":
                peliculas = _context.Peliculas.OrderByDescending(p => p.Id).ToList();
                break;
            default:
                BadRequest("Las querys validas son ASC o DESC");
                break;
        }

        return peliculas;
    }
}
