using ABC.Data;
using ABC.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult RegisterStudent(RegisterRequest model)
    {
        var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(model.Password);

        //alaMaKota
        //hash(alaMaKota+salt1+pepper)=>sdsd3dfgsd3fdfdfdfdsfsfsdfsdfsdf
        //hash(alaMaKota+salt2+pepper)=>df4htghdfgdfg32fedfdfsfq23fedfdd


        var user = new AppUser()
        {
            Email = model.Email,
            Login = model.Login,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelpers.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1)
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok();
    }
}