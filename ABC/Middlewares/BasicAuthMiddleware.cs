using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using ABC.Data;
using ABC.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ABC.Middlewares;

public class BasicAuthMiddleware
{
    private readonly RequestDelegate _next;
    private const string Realm = "My Realm";

    public BasicAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, AppDatabaseContext dbContext)
    {
        if (httpContext.Request.Headers.ContainsKey("Authorization"))
        {
            var authHeader = AuthenticationHeaderValue.Parse(httpContext.Request.Headers["Authorization"]);

            if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
                authHeader.Parameter != null)
            {
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
                var username = credentials[0];
                var password = credentials[1];

                Console.WriteLine($"Attempting to authorize user: {username}");

                if (await IsAuthorized(username, password, dbContext))
                {
                    // Add a custom claim to mark as authenticated by Basic Auth
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, username)
                    };

                    var identity = new ClaimsIdentity(claims, "Basic");
                    httpContext.User = new ClaimsPrincipal(identity);

                    Console.WriteLine($"User {username} authorized successfully.");
                    await _next(httpContext);
                    return;
                }
                else
                {
                    Console.WriteLine($"User {username} failed authorization.");
                }
            }
        }

        Console.WriteLine("Authorization header missing or invalid.");
        // Return 401 Unauthorized if authentication fails
        httpContext.Response.Headers["WWW-Authenticate"] = $"Basic realm=\"{Realm}\"";
        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
    }

    private async Task<bool> IsAuthorized(string login, string password, AppDatabaseContext context)
    {
        var user = await context.Users.FirstOrDefaultAsync(e => e.Login == login);
        if (user == null)
        {
            Console.WriteLine($"User {login} not found.");
            return false;
        }

        var hashedPasswordFromRequest = SecurityHelpers.GetHashedPasswordWithSalt(password, user.Salt);
        var isAuthorized = hashedPasswordFromRequest.Equals(user.Password);

        Console.WriteLine($"User {login} authorization status: {isAuthorized}");
        return isAuthorized;
    }
}