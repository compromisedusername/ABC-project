using ABC.Data;
using GakkoHorizontalSlice.Model;
using Microsoft.EntityFrameworkCore;

namespace ABC.Repositories.Users;

public class UsersRepository : IUsersRepository
{
    private readonly AppDatabaseContext _context;

    public UsersRepository(AppDatabaseContext context)
    {
        _context = context;
    }

    public Task<AppUser> DoesUserExistsWithGivenLogin(string modelLogin)
    {
       return _context.Users.FirstOrDefaultAsync(e => e.Login == modelLogin);
    }

    public async Task<AppUser> DoesUserWithGivenEmailExist(string modelEmail)
    {
        return await _context.Users.FirstOrDefaultAsync(e => e.Email == modelEmail);
    }

    public async Task AddUser(AppUser user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<AppUser> GetUserByLogin(LoginRequestDto loginRequestDto)
    {
        return await _context.Users.Where(u => u.Login == loginRequestDto.Login).FirstOrDefaultAsync();
    }

    public async Task UpdateUser(AppUser user)
    {
        _context.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<AppUser> GetUserByRefreshToken(RefreshTokenRequestDto refreshToken)
    {
      return await _context.Users.Where(u => u.RefreshToken == refreshToken.RefreshToken).FirstOrDefaultAsync();
    }
}