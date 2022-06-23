using ChallengeApiPgSql.Data;
using ChallengeApiPgSql.Dto;
using ChallengeApiPgSql.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChallengeApiPgSql.Controllers;
[Route("api/[controller]")]
[ApiController]
//[/*Authorize*/]
public class PersonajeController : ControllerBase
{
    private readonly DataContext _context;
    public PersonajeController(DataContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<List<Personaje>>> GetAll()
    {
        return Ok(await _context.Personajes.Include(p => p.Peliculas).ToListAsync());
    }

    [HttpGet("id")]
    public async Task<ActionResult<Personaje>> GetById(int id)
    {
        var personaje = _context.Personajes.Include(p=> p.Peliculas).FirstOrDefault(p=> p.Id == id);
        if (personaje == null)
        {
            return BadRequest("No existe personaje con ese id");
        }
        else
        {
            return Ok(personaje);
        }
    }

    [HttpPost]
    public async Task<ActionResult<List<Personaje>>> CreatePersonaje(PersonajeDto nPersonaje)
    {
        var personaje = new Personaje()
        {
            Id = nPersonaje.Id,
            Edad = nPersonaje.Edad,
            Historia = nPersonaje.Historia,
            Nombre = nPersonaje.Nombre,
            Imagen = nPersonaje.Imagen
        };
        _context.Personajes.Add(personaje);
        await _context.SaveChangesAsync();
        return Ok(_context.Personajes.ToListAsync());
        
    }


    [HttpPut]

    public async Task<ActionResult<Personaje>> UpdatePersonaje(PersonajeDto personaje)
    {
        var updPersonaje = await _context.Personajes.FindAsync(personaje.Id);
        if (updPersonaje == null)
        {
            return BadRequest("No se encontro ese personaje con ese Id");
        }
        else
        {
            updPersonaje.Imagen = personaje.Imagen;
            updPersonaje.Nombre = personaje.Nombre;
            updPersonaje.Historia = personaje.Historia;
            updPersonaje.Edad = personaje.Edad;

            await _context.SaveChangesAsync();
            return Ok(updPersonaje);
        }
    }

    [HttpDelete("id")]
    public async Task<ActionResult<List<Personaje>>> DeletePersonaje(int id)
    {
        var dPersonaje = await _context.Personajes.FindAsync(id);
        if (dPersonaje == null)
        {
            return BadRequest("Ese id es incorrecto no se encontro personaje");
        }
        else
        {
            _context.Personajes.Remove(dPersonaje);
            await _context.SaveChangesAsync();
            return Ok(await _context.Personajes.ToListAsync());
        }
    }

    [HttpPost("id")]
    public async Task<ActionResult<Personaje>> AddPelicula(PeliculaDto peliculaDto, int id)
    {
        var personaje = await _context.Personajes.FindAsync(id);
        if (personaje == null)
        {
            return BadRequest("No existe personaje con ese id");
        }
        else
        {
            var pelicula = _context.Peliculas.FirstOrDefault(p => p.Id == peliculaDto.Id);

            if (pelicula == null)
            {
                var nPelicula = new Pelicula()
                {
                    Id = peliculaDto.Id,
                    Imagen = peliculaDto.Imagen,
                    Titulo = peliculaDto.Titulo,
                    Estreno = peliculaDto.Estreno,
                    Calificacion = peliculaDto.Calificacion,
                    Personajes = new List<Personaje>()
                {
                    personaje
                }
                };
                personaje.Peliculas.Add(nPelicula);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                pelicula.Personajes.Add(personaje);
                await _context.SaveChangesAsync();
                return Ok();
            }
           
        }
    }

    [HttpGet("nombre")]
    public async Task<ActionResult<List<Personaje>>> GetPersonajesByNombre(string nombre)
    {
        var personajes = await _context.Personajes.Where(p=> p.Nombre == nombre).ToListAsync();
        if (personajes == null)
        {
            return BadRequest("No existen personajes con ese nombre");
        }
        return Ok(personajes);
    }

    [HttpGet("edad")]
    public async Task<ActionResult<List<Personaje>>>GetPersonajesByEdad(int edad)
    {
        var personajes = await _context.Personajes.Where(p => p.Edad == edad).ToListAsync();
        if(personajes == null)
        {
            return BadRequest("No se encontro ningun personaje con esa edad");
        }
        return Ok(personajes);
    }

    [HttpGet("idpelicula")]
    public async Task<ActionResult<List<Personaje>>>GetPersonajeByPelicula(int idPelicula)
    {
        var pelicula = await _context.Peliculas.Include(p=> p.Personajes).FirstOrDefaultAsync(p=> p.Id == idPelicula);
        if (pelicula == null)
        {
            return BadRequest("No hay ningun personaje asociado a esa pelicula");
        }
        return Ok(pelicula.Personajes);
    }
}
