using ChallengeApiPgSql.Data;
using ChallengeApiPgSql.Dto;
using ChallengeApiPgSql.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChallengeApiPgSql.Controllers;
[Route("auth")]
[ApiController]
public class JWTTokenController : ControllerBase
{
    public readonly DataContext _context;
    public IConfiguration _config { get; set; }
    public JWTTokenController(DataContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Post(UsuarioDto user)
    {
        if (user.Username != null && user.Password != null && user != null)
        {
            var userData = GetUser(user.Username, user.Password);
            var jwt = _config.GetSection("Jwt").Get<Jwt>();
            if (userData != null)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Username", user.Username),
                    new Claim("Password", user.Password)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(jwt.Issuer, jwt.Audience, claims,
                    expires: DateTime.Now.AddMinutes(20), signingCredentials: signIn);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest("Credenciales Incorrectas");
            }
        }
        else
        {
            return BadRequest("Credenciales Invalidas");
        }
    }

    private object GetUser(string username, string password)
    {
        return _context.Usuarios.FirstOrDefault(c => c.Username == username && c.Password == password);
    }

    [HttpPost]
    [Route("register")]

    public async Task<ActionResult<Usuario>> Register(Usuario nUsuario)
    {
        _context.Usuarios.Add(nUsuario);
        await _context.SaveChangesAsync();
        return Ok(await _context.Usuarios.ToListAsync());


        //HACER ENVIO DE MAIL
    }
}
