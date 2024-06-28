using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ABC.Exceptions;
using ABC.Helpers;
using ABC.Repositories.Users;
using GakkoHorizontalSlice.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ABC.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUsersRepository _usersRepository;

    public AuthController(IConfiguration configuration, IUsersRepository usersRepository)
    {
        _configuration = configuration;
        _usersRepository = usersRepository;
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterStudent(RegisterRequestDto model)
    {
        var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(model.Password);
        if (await _usersRepository.DoesUserExistsWithGivenLogin(model.Login) != null)
        {
            throw new DomainException()
            {
                Message = "Login is already in use.",
                StatusCode = 400
            };
        } if (await _usersRepository.DoesUserWithGivenEmailExist(model.Email) != null)
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

        await _usersRepository.AddUser(user);

        return Ok();
    }
     [AllowAnonymous]
    [HttpPost("login")]
    public async  Task<IActionResult> Login(LoginRequestDto loginRequestDto)
    {
        AppUser user = await _usersRepository.GetUserByLogin(loginRequestDto);
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
        _usersRepository.UpdateUser(user);

        return Ok(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken = user.RefreshToken
        });
    }

    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequestDto refreshToken)
    {
        AppUser user = await _usersRepository.GetUserByRefreshToken(refreshToken);
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
        await _usersRepository.UpdateUser(user);

        
        return Ok(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            refreshToken = user.RefreshToken
        });
    }
    
    
   
    
}