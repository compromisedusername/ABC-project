using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using ABC.Data;
using ABC.Exceptions;
using ABC.Helpers;
using GakkoHorizontalSlice.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ABC.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly AppDatabaseContext _context;

    public AuthController(IConfiguration configuration, AppDatabaseContext context)
    {
        _configuration = configuration;
        _context = context;
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterStudent(RegisterRequestDto model)
    {
        var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(model.Password);


        if (await _context.Users.FirstOrDefaultAsync(e=>e.Login == model.Login) != null)
        {
            throw new DomainException()
            {
                Message = "Login is already in use.",
                StatusCode = 400
            };
        } if (await _context.Users.FirstOrDefaultAsync(e=>e.Email == model.Email) != null)
        {
            throw new DomainException()
            {
                Message = "Email is already in use.",
                StatusCode = 400
            };
        }

        var user = new AppUser()
        {
            Email = model.Email,
            Login = model.Login,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelpers.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1)
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Ok();
    }
     [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(LoginRequestDto loginRequestDto)
    {
        AppUser user = _context.Users.Where(u => u.Login == loginRequestDto.Login).FirstOrDefault();

        string passwordHashFromDb = user.Password;
        string curHashedPassword = SecurityHelpers.GetHashedPasswordWithSalt(loginRequestDto.Password, user.Salt);

        if (passwordHashFromDb != curHashedPassword)
        {
            return Unauthorized();
        }


        var userclaim = new List<Claim>
        {
            new (ClaimTypes.Name, user.Login),
            new (ClaimTypes.Role, "user"),
        };

        if (user.Login == "admin")
        {
            userclaim.Add(new Claim(ClaimTypes.Role, "admin"));
        }

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        _context.SaveChanges();

        return Ok(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken = user.RefreshToken
        });
    }

    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost("refresh")]
    public IActionResult Refresh(RefreshTokenRequestDto refreshToken)
    {
        AppUser user = _context.Users.Where(u => u.RefreshToken == refreshToken.RefreshToken).FirstOrDefault();
        if (user == null)
        {
            throw new SecurityTokenException("Invalid refresh token");
        }

        if (user.RefreshTokenExp < DateTime.Now)
        {
            throw new SecurityTokenException("Refresh token expired");
        }
        
        var userclaim = new List<Claim>
        {
            new (ClaimTypes.Name, user.Login),
            new (ClaimTypes.Role, "user"),
        };

        if (user.Login == "admin")
        {
            userclaim.Add(new Claim(ClaimTypes.Role, "admin"));
        }
        

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtToken = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        _context.SaveChanges();

        
        return Ok(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            refreshToken = user.RefreshToken
        });
    }
    
    
   
    
}