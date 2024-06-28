using System.ComponentModel.DataAnnotations;

namespace GakkoHorizontalSlice.Model
{
    public class AppUser
    {
        [Key]
        public int IdUser { get; set; }
        [MaxLength(50)]
        public string Login { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        [MaxLength(50)]
        public string Salt { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExp { get; set; }
    }
}
