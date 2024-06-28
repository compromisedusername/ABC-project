using GakkoHorizontalSlice.Model;

namespace ABC.Repositories.Users;

public interface IUsersRepository
{
    Task<AppUser> DoesUserExistsWithGivenLogin(string modelLogin);
    Task<AppUser> DoesUserWithGivenEmailExist(string modelEmail);
    Task AddUser(AppUser user);
    Task<AppUser> GetUserByLogin(LoginRequestDto loginRequestDto);
    Task UpdateUser(AppUser user);
    Task<AppUser> GetUserByRefreshToken(RefreshTokenRequestDto refreshToken);
}