using System.ComponentModel.DataAnnotations;

namespace ABC.DTOs;

public class RequestClientAddDto
{
    [Required]
    [EmailAddress]
    [MaxLength(320)]
    public string Email { get; set; }
    [Required]
    [Phone]
    [MaxLength(20)]
    public string PhoneNumber { get; set; }
    [Required]
    public int IdAddress { get; set; }
    [Required]
    [RegularExpression("(Natural)|(Company)", ErrorMessage = "Client must be Natural or Company!")]
    public string ClientType { get; set; } 
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } 
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } 

    [MaxLength(150)]
    public string? CompanyName { get; set; }
    [MaxLength(14)]
    public string? KRS { get; set; }
    [MaxLength(9)]
    public string? PESEL { get; set; } 
}