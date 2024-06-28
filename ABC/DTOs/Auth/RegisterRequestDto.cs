using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GakkoHorizontalSlice.Model
{
    public class RegisterRequestDto
    {
        [EmailAddress]
        public string Email { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
        public string Login { get; set; }

    }
}
